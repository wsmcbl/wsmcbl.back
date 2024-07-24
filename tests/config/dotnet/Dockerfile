FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ../../../src/wsmcbl.src.csproj .
RUN dotnet restore

# Instalar dotnet-ef globalmente y agregarlo al PATH
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ../../../src .
RUN dotnet build -c Realease -o out
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Establecer la variable de entorno para el entorno de pruebas
ENV ASPNETCORE_ENVIRONMENT=Test

# Ejecutar las migraciones y luego iniciar la aplicación
ENTRYPOINT ["dotnet", "wsmcbl.src.dll"]