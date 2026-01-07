# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["RedisCachingWebApi.csproj", "./"]
RUN dotnet restore "RedisCachingWebApi.csproj"

COPY . .
RUN dotnet build "RedisCachingWebApi.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "RedisCachingWebApi.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# ✅ INSTALL FONTS HERE (IMPORTANT FOR iTextSharp)
RUN apt-get update && apt-get install -y fonts-dejavu && rm -rf /var/lib/apt/lists/*

EXPOSE 80
EXPOSE 443

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "RedisCachingWebApi.dll"]