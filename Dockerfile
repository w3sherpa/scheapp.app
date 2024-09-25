#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./sherpaticket.app/sherpaticket.csproj", "sherpaticket.app/"]
COPY ["./sherpaticket.shared.library/sherpaticket.shared.csproj", "sherpaticket.shared.library/"]

RUN dotnet restore "sherpaticket.app/sherpaticket.csproj"
COPY . .
WORKDIR "/src/sherpaticket.app"
RUN dotnet build "sherpaticket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sherpaticket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV TZ=America/Chicago
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

ENTRYPOINT ["dotnet", "sherpaticket.dll"]