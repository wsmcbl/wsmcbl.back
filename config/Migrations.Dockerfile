FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY src/*.csproj ./
RUN dotnet restore

COPY src/ ./

RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

COPY /config/make-migrations.sh /app/init.sh
RUN chmod +x /app/init.sh