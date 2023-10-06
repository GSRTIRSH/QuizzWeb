# Используем базовый образ ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#EXPOSE 80

# Используем базовый образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Копируем проект и восстанавливаем зависимости
COPY src/*.csproj src/
RUN dotnet restore "src/QuizzWebApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "QuizzWebApi.csproj" -c Release -o /app/build

# Собираем проект
FROM build AS publish
RUN dotnet publish "QuizzWebApi.csproj" -c Release -o /app/publish

# Собираем конечный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizzWebApi.dll"]