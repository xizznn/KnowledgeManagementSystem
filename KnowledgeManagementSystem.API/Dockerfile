FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["KnowledgeManagementSystem.API/KnowledgeManagementSystem.API.csproj", "KnowledgeManagementSystem.API/"]
COPY ["KnowledgeManagementSystem.Core/KnowledgeManagementSystem.Core.csproj", "KnowledgeManagementSystem.Core/"]
COPY ["KnowledgeManagementSystem.Infrastructure/KnowledgeManagementSystem.Infrastructure.csproj", "KnowledgeManagementSystem.Infrastructure/"]

RUN dotnet restore "KnowledgeManagementSystem.API/KnowledgeManagementSystem.API.csproj"

COPY . .

RUN dotnet publish "KnowledgeManagementSystem.API/KnowledgeManagementSystem.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KnowledgeManagementSystem.API.dll"]
