# 1. Imagen SDK de .NET para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar absolutamente todo el proyecto
COPY . .

# Buscar y restaurar dependencias de la solución entera
RUN dotnet restore

# Compilar y publicar la solución
RUN dotnet publish -c Release -o /app/out

# 2. Imagen final liviana para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto de Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Revisa cómo se llama tu archivo ejecutable principal (generalmente MasoksTech.API.dll)
ENTRYPOINT ["dotnet", "MasoksTech.API.dll"]
