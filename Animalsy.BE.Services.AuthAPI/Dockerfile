#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["Animalsy.BE.Services.AuthAPI/Animalsy.BE.Services.AuthAPI.csproj", "Animalsy.BE.Services.AuthAPI/"]
RUN dotnet restore "./Animalsy.BE.Services.AuthAPI/Animalsy.BE.Services.AuthAPI.csproj"
COPY . .
WORKDIR "/src/Animalsy.BE.Services.AuthAPI"
RUN dotnet build "Animalsy.BE.Services.AuthAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Animalsy.BE.Services.AuthAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Animalsy.BE.Services.AuthAPI.dll"]