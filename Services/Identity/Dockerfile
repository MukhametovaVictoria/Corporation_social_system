FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Identity/Identity.csproj", "Identity/"]
COPY ["BusinessLogic.Services/BusinessLogic.Services.csproj", "BusinessLogic.Services/"]
COPY ["DataAccess.Entities/DataAccess.Entities.csproj", "DataAccess.Entities/"]
COPY ["DataAccess.Repositories/DataAccess.Repositories.csproj", "DataAccess.Repositories/"]
COPY ["DataAccess.Context/DataAccess.Context.csproj", "DataAccess.Context/"]
COPY ["BusinessLogic.Models/BusinessLogic.Models.csproj", "BusinessLogic.Models/"]
RUN dotnet restore "Identity/Identity.csproj"
COPY . .
WORKDIR "/src/Identity"
RUN dotnet build "Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.dll"]
