#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PhotoSi_IT_Demo_New.ApiGateway/PhotoSi_IT_Demo_New.ApiGateway.csproj", "PhotoSi_IT_Demo_New.ApiGateway/"]
COPY ["PhotoSi_IT_Demo_New.Infrastructure/PhotoSi_IT_Demo_New.Infrastructure.csproj", "PhotoSi_IT_Demo_New.Infrastructure/"]
COPY ["PhotoSi_IT_Demo_New.Infrastructure.Abstractions/PhotoSi_IT_Demo_New.Infrastructure.Abstractions.csproj", "PhotoSi_IT_Demo_New.Infrastructure.Abstractions/"]
COPY ["PhotoSi_IT_Demo_New.Common/PhotoSi_IT_Demo_New.Common.csproj", "PhotoSi_IT_Demo_New.Common/"]
RUN dotnet restore "PhotoSi_IT_Demo_New.ApiGateway/PhotoSi_IT_Demo_New.ApiGateway.csproj"
COPY . .
WORKDIR "/src/PhotoSi_IT_Demo_New.ApiGateway"
RUN dotnet build "PhotoSi_IT_Demo_New.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhotoSi_IT_Demo_New.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoSi_IT_Demo_New.ApiGateway.dll"]