#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8002



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG GoogleClientId
ARG GoogleClientSecret

ENV AuthenticationScheme__Google__ClientId=$GoogleClientId
ENV AuthenticationScheme__Google__ClientSecret=$GoogleClientSecret

WORKDIR /src
COPY ["./app/scheapp.app.csproj", "app/"]

RUN dotnet restore "./app/./scheapp.app.csproj"
COPY . .
WORKDIR "/src/app"
RUN dotnet build "./scheapp.app.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./scheapp.app.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENV TZ=America/Chicago
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

ENTRYPOINT ["dotnet", "scheapp.app.dll"]