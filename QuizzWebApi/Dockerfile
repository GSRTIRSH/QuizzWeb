FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY /. ./
RUN dotnet restore "QuizzWebApi.csproj"

RUN dotnet build "QuizzWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizzebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizzWebApi.dll"]