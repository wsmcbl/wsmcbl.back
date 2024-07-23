# Usa la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el archivo de proyecto y restaura las dependencias
COPY ../../../src/*.csproj .
RUN dotnet restore

# Copia el resto de los archivos y compila la aplicación
COPY ../../../src/. .
RUN dotnet publish -c Release -o out

# Usa la imagen de runtime de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "wsmcbl.src.dll"]
