#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 10000

ENV SPARKLE_API_TOKEN="1qaz!QAZ"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sparkle.Api/Sparkle.Api.csproj", "Sparkle.Api/"]
COPY ["Sparkle.Transfer/Sparkle.Transfer.csproj", "Sparkle.Transfer/"]
COPY ["Sparkle.Domain/Sparkle.Domain.csproj", "Sparkle.Domain/"]
COPY ["Sparkle.Infrastructure/Sparkle.Infrastructure.csproj", "Sparkle.Infrastructure/"]
COPY ["Sparkle.Shared/Sparkle.Shared.csproj", "Sparkle.Shared/"]
COPY ["Sparkle.Handling/Sparkle.Handling.csproj", "Sparkle.Handling/"]"]
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