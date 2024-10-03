#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./app/scheapp.csproj", "scheapp.app/"]

RUN dotnet restore "scheapp.app/scheapp.csproj"
COPY . .
WORKDIR "/src/scheapp.app"
RUN dotnet build "scheapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "scheapp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV TZ=America/Chicago
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

ENTRYPOINT ["dotnet", "scheapp.app.dll"]