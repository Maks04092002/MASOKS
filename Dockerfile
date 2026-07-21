# 1. Imagen SDK de .NET 10 para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar todo el proyecto
COPY . .

# Restaurar y publicar apuntando a tu carpeta real MasoksTech.Api
RUN dotnet restore "MasoksTech.Api/MasoksTech.Api.csproj"
RUN dotnet publish "MasoksTech.Api/MasoksTech.Api.csproj" -c Release -o /app/out

# 2. Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MasoksTech.Api.dll"]