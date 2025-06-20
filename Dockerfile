# Use official .NET SDK image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy everything and restore dependencies
COPY ["KnowledgeManagementSystem.API/KnowledgeManagementSystem.API.csproj", "KnowledgeManagementSystem.API/"]
COPY . .
RUN dotnet restore "KnowledgeManagementSystem.API/KnowledgeManagementSystem.API.csproj"

# build app
WORKDIR "/src/KnowledgeManagementSystem.API"
RUN dotnet build "KnowledgeManagementSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KnowledgeManagementSystem.API.csproj" -c Release -o /app/publish

# final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnowledgeManagementSystem.API.dll"]
