FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY /. src/
WORKDIR "/src"
RUN dotnet restore "QuizzWebApi.csproj"
#COPY . .
RUN dotnet build "QuizzWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizzWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizzWebApi.dll"]