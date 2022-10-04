#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PhotoSi_IT_Demo_New.Services.Utenti/PhotoSi_IT_Demo_New.Services.Utenti.csproj", "PhotoSi_IT_Demo_New.Services.Utenti/"]
COPY ["PhotoSi_IT_Demo_New.Common/PhotoSi_IT_Demo_New.Common.csproj", "PhotoSi_IT_Demo_New.Common/"]
COPY ["PhotoSi_IT_Demo_New.Infrastructure/PhotoSi_IT_Demo_New.Infrastructure.csproj", "PhotoSi_IT_Demo_New.Infrastructure/"]
COPY ["PhotoSi_IT_Demo_New.Infrastructure.Abstractions/PhotoSi_IT_Demo_New.Infrastructure.Abstractions.csproj", "PhotoSi_IT_Demo_New.Infrastructure.Abstractions/"]
RUN dotnet restore "PhotoSi_IT_Demo_New.Services.Utenti/PhotoSi_IT_Demo_New.Services.Utenti.csproj"
COPY . .
WORKDIR "/src/PhotoSi_IT_Demo_New.Services.Utenti"
RUN dotnet build "PhotoSi_IT_Demo_New.Services.Utenti.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhotoSi_IT_Demo_New.Services.Utenti.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoSi_IT_Demo_New.Services.Utenti.dll"]