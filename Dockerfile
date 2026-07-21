# 1. Imagen SDK de .NET para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar todo el repositorio
COPY . .

# Buscar automáticamente el archivo .csproj y compilarlo
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# 2. Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer puerto para Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Ejecutar el proyecto compilado
ENTRYPOINT ["dotnet", "MasoksTech.API.dll"]
