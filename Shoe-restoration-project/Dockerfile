FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Shoe-restoration-project/Shoe-restoration-project.csproj", "Shoe-restoration-project/"]
RUN dotnet restore "./Shoe-restoration-project/Shoe-restoration-project.csproj"
COPY . .
WORKDIR "/src/Shoe-restoration-project"

RUN dotnet build "./Shoe-restoration-project.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shoe-restoration-project.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shoe-restoration-project.dll"]
