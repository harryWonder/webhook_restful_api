#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["webhook_restful_api.csproj", "."]
RUN dotnet restore "./webhook_restful_api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "webhook_restful_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webhook_restful_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webhook_restful_api.dll"]