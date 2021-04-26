#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y libgdiplus
EXPOSE 80
EXPOSE 443

WORKDIR "/app"
COPY ["HseAr/global.json", "./"]
COPY ["HseAr/global.Development.json", "./"]
COPY ["HseAr/secret.json", "./"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HseAr/HseAr.ArClient.Api/HseAr.ArClient.Api.csproj", "HseAr.ArClient.Api/"]
COPY ["HseAr/HseAr.Dependencies/HseAr.Dependencies.csproj", "HseAr.Dependencies/"]
COPY ["HseAr/HseAr.DataAccess.Mongodb/HseAr.DataAccess.Mongodb.csproj", "HseAr.DataAccess.Mongodb/"]
COPY ["HseAr/HseAr.Data/HseAr.Data.csproj", "HseAr.Data/"]
COPY ["HseAr/HseAr.Core/HseAr.Core.csproj", "HseAr.Core/"]
COPY ["HseAr/HseAr.Infrastructure/HseAr.Infrastructure.csproj", "HseAr.Infrastructure/"]
COPY ["HseAr/HseAr.BlenderService/HseAr.BlenderService.csproj", "HseAr.BlenderService/"]
COPY ["HseAr/HseAr.DataAccess.EFCore/HseAr.DataAccess.EFCore.csproj", "HseAr.DataAccess.EFCore/"]
COPY ["HseAr/HseAr.Integration.SceneExport/HseAr.Integration.SceneExport.csproj", "HseAr.Integration.SceneExport/"]
COPY ["HseAr/HseAr.BusinessLayer/HseAr.BusinessLayer.csproj", "HseAr.BusinessLayer/"]
RUN dotnet restore "HseAr.ArClient.Api/HseAr.ArClient.Api.csproj"
COPY HseAr/ .
WORKDIR "/src/HseAr.ArClient.Api"
RUN dotnet build "HseAr.ArClient.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HseAr.ArClient.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app/release
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HseAr.ArClient.Api.dll"]