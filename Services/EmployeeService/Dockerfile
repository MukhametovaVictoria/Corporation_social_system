FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["BLL/BusinessLogic.Services.Abstractions/BusinessLogic.Services.Abstractions.csproj", "BLL/BusinessLogic.Services.Abstractions/"]
COPY ["DAL/DataAccess.Entities/DataAccess.Entities.csproj", "DAL/DataAccess.Entities/"]
COPY ["BLL/BusinessLogic.Contracts/BusinessLogic.Contracts.csproj", "BLL/BusinessLogic.Contracts/"]
COPY ["BLL/BusinessLogic.Services.Implementations/BusinessLogic.Services.Implementations.csproj", "BLL/BusinessLogic.Services.Implementations/"]
COPY ["DAL/DataAccess.Repositories.Abstractions/DataAccess.Repositories.Abstractions.csproj", "DAL/DataAccess.Repositories.Abstractions/"]
COPY ["DAL/DataAccess.Repositories.Implementations/DataAccess.Repositories.Implementations.csproj", "DAL/DataAccess.Repositories.Implementations/"]
COPY ["DAL/DataAccess.Context/DataAccess.Context.csproj", "DAL/DataAccess.Context/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
