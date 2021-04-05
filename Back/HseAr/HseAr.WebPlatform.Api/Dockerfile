#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 5005


WORKDIR "/"
COPY ["HseAr/global.json", "./"]
COPY ["HseAr/global.Development.json", "./"]
COPY ["HseAr/secret.json", "./"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HseAr/HseAr.WebPlatform.Api/HseAr.WebPlatform.Api.csproj", "HseAr.WebPlatform.Api/"]
COPY ["HseAr/HseAr.Core/HseAr.Core.csproj", "HseAr.Core/"]
COPY ["HseAr/HseAr.Infrastructure/HseAr.Infrastructure.csproj", "HseAr.Infrastructure/"]
COPY ["HseAr/HseAr.Dependencies/HseAr.Dependencies.csproj", "HseAr.Dependencies/"]
COPY ["HseAr/HseAr.DataAccess.Mongodb/HseAr.DataAccess.Mongodb.csproj", "HseAr.DataAccess.Mongodb/"]
COPY ["HseAr/HseAr.Data/HseAr.Data.csproj", "HseAr.Data/"]
COPY ["HseAr/HseAr.DataAccess.EFCore/HseAr.DataAccess.EFCore.csproj", "HseAr.DataAccess.EFCore/"]
COPY ["HseAr/HseAr.Integration.SceneExport/HseAr.Integration.SceneExport.csproj", "HseAr.Integration.SceneExport/"]
COPY ["HseAr/HseAr.BusinessLayer/HseAr.BusinessLayer.csproj", "HseAr.BusinessLayer/"]
RUN dotnet restore "HseAr.WebPlatform.Api/HseAr.WebPlatform.Api.csproj"
COPY HseAr/ .
WORKDIR "/src/HseAr.WebPlatform.Api"
RUN dotnet build "HseAr.WebPlatform.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HseAr.WebPlatform.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HseAr.WebPlatform.Api.dll"]