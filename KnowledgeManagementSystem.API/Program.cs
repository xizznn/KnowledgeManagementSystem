using KnowledgeManagementSystem.API.Mapping;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using KnowledgeManagementSystem.Core.Services;
using KnowledgeManagementSystem.Infrastructure.Data;
using KnowledgeManagementSystem.Infrastructure.Repositories;
using KnowledgeManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Net;
using KnowledgeManagementSystem.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Настройка логгирования
builder.Logging.ClearProviders()
       .AddConsole()
       .AddDebug();

builder.Services.AddRazorPages(options =>
{
})
.AddRazorRuntimeCompilation();

// Настройка базы данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
});

// Регистрация сервисов
RegisterServices(builder.Services);

// Настройка контроллеров
builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
           options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
       });

ConfigureSwagger(builder.Services);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("Database", () =>
        HealthCheckResult.Healthy("Database connection is OK"),
        tags: new[] { "db", "ready" })
    .AddDbContextCheck<ApplicationDbContext>();

ConfigureJwtAuthentication(builder.Services, builder.Configuration);

// CORS политика
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

await ApplyMigrationsWithRetryAsync(app);

app.UseExceptionHandler("/Error");
app.UseStatusCodePagesWithReExecute("/Error/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KMS API v1");
    c.RoutePrefix = "swagger";
    c.DisplayRequestDuration();
    c.DefaultModelExpandDepth(2);
    c.DefaultModelsExpandDepth(-1);
    c.EnablePersistAuthorization();
});

// Маппинг endpoints
app.MapRazorPages();
app.MapControllers();
app.MapHealthChecks("/health");

// Глобальный обработчик ошибок API
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };

        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message,
            StackTrace = app.Environment.IsDevelopment() ? ex.StackTrace : null
        }.ToString());
    }
});

app.MapGet("/", () => Results.Redirect("/Index"));

app.Run();

// Методы конфигурации
void RegisterServices(IServiceCollection services)
{
    // Репозитории
    services.AddScoped<ICourseRepository, CourseRepository>();
    services.AddScoped<ITestRepository, TestRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();
    services.AddScoped<IQuestionRepository, QuestionRepository>();
    services.AddScoped<IQuestionInTestRepository, QuestionInTestRepository>();
    services.AddScoped<ITestInCourseRepository, TestInCourseRepository>();
    services.AddScoped<IUserOnCourseRepository, UserOnCourseRepository>();
    services.AddScoped<IFavoriteCourseRepository, FavoriteCourseRepository>();
    services.AddScoped<ITestResultRepository, TestResultRepository>();
    services.AddScoped<IFavoriteTestRepository, FavoriteTestRepository>();
    // Сервисы
    services.AddScoped<ICourseService, CourseService>();
    services.AddScoped<ITestService, TestService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IQuestionService, QuestionService>();
    services.AddScoped<IQuestionInTestService, QuestionInTestService>();
    services.AddScoped<ITestInCourseService, TestInCourseService>();
    services.AddScoped<IUserOnCourseService, UserOnCourseService>();
    services.AddScoped<IFavoriteCourseService, FavoriteCourseService>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<ITestResultService, TestResultService>();
    services.AddScoped<IFavoriteTestService, FavoriteTestService>();

    // HTTP контекст
    services.AddHttpContextAccessor();

    // Кэширование
    services.AddMemoryCache();
    services.AddDistributedMemoryCache();

    // Сессии
    services.AddSession(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.IdleTimeout = TimeSpan.FromMinutes(30);
    });
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Knowledge Management System API",
            Description = "API for Knowledge Management System with JWT Authentication",
            Contact = new OpenApiContact
            {
                Name = "Support",
                Email = "support@kms.com"
            }
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }

        c.OrderActionsBy(apiDesc => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
        c.EnableAnnotations();
    });
}

void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("Jwt");
    var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero,

            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

    services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
    });
}

async Task ApplyMigrationsWithRetryAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    const int maxRetries = 10;
    const int delaySeconds = 5;

    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Checking database connection...");

            if (!await db.Database.CanConnectAsync())
            {
                logger.LogWarning("Database not ready. Waiting {Delay}s...", delaySeconds);
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                continue;
            }

            logger.LogInformation("Applying pending migrations...");
            await db.Database.MigrateAsync();
            logger.LogInformation("Migrations applied successfully");
            return;
        }
        catch (Npgsql.NpgsqlException ex) when (ex.IsTransient)
        {
            logger.LogError(ex, "Transient error during migration attempt {Attempt}", i + 1);
            await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration attempt {Attempt} failed", i + 1);
            if (i == maxRetries - 1)
            {
                logger.LogCritical("All migration attempts failed");
                throw;
            }

            await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
        }
    }
}
