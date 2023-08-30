#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sparkle.Api/Sparkle.Api.csproj", "Sparkle.Api/"]
COPY ["Sparkle.Transfer/Sparkle.Transfer.csproj", "Sparkle.Transfer/"]
COPY ["SparkleRegressor.Client/SparkleRegressor.Client.csproj", "SparkleRegressor.Client/"]
RUN dotnet restore "Sparkle.Api/Sparkle.Api.csproj"
COPY . .
WORKDIR "/src/Sparkle.Api"
RUN dotnet build "Sparkle.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sparkle.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN ls -R /app

ENTRYPOINT ["dotnet", "Sparkle.Api.dll"]