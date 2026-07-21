# 1. Imagen base SDK de .NET 10 para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar todo el código y restaurar/compilar
COPY . .
RUN dotnet restore "src/MasoksTech.API/MasoksTech.API.csproj"
RUN dotnet publish "src/MasoksTech.API/MasoksTech.API.csproj" -c Release -o /app/publish

# 2. Imagen final liviana para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto predeterminado de Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MasoksTech.API.dll"]
