FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY SnakeGame/SnakeGame.Source.csproj SnakeGame/
RUN dotnet restore "SnakeGame/SnakeGame.Source.csproj"

# copy and publish app and libraries
FROM build AS publish
COPY . .
WORKDIR /source/SnakeGame
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM base AS final
FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SnakeGame.Source.dll"]