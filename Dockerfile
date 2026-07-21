# 1. Imagen base SDK de .NET 10 para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo .csproj respetando la carpeta exacta
COPY ["src/MasoksTech.API/MasoksTech.API.csproj", "src/MasoksTech.API/"]

# Si tienes otros proyectos en src/ (como Domain, Application, Infrastructure), también se copian:
# COPY ["src/MasoksTech.Domain/MasoksTech.Domain.csproj", "src/MasoksTech.Domain/"]
# COPY ["src/MasoksTech.Application/MasoksTech.Application.csproj", "src/MasoksTech.Application/"]
# COPY ["src/MasoksTech.Infrastructure/MasoksTech.Infrastructure.csproj", "src/MasoksTech.Infrastructure/"]

# Restaurar paquetes de la API
RUN dotnet restore "src/MasoksTech.API/MasoksTech.API.csproj"

# Copiar todo el código fuente del proyecto
COPY . .

# Compilar y publicar
WORKDIR "/src/src/MasoksTech.API"
RUN dotnet publish "MasoksTech.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Imagen final para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto de Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MasoksTech.API.dll"]
