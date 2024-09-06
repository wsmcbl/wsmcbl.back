FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
# Variable de entorno para almacenar las dependencias de NuGet fuera del directorio montado
ENV NUGET_PACKAGES=/root/.nuget/packages

# Solo restaura los paquetes de NuGet, no es necesario copiar el código
COPY *.sln ./
COPY src/*.csproj ./src/
COPY tests/*.csproj ./tests/

RUN cd src && dotnet restore
RUN cd tests && dotnet restore

# Comando por defecto: las pruebas se ejecutarán con `dotnet test`
CMD ["dotnet", "test"]