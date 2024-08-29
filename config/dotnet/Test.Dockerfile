FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/*.csproj ./src/
COPY tests/*.csproj ./tests/

RUN cd src && dotnet restore
RUN cd tests && dotnet restore

COPY src/ ./src
COPY tests/ ./tests

RUN cd src && dotnet build
RUN cd tests && dotnet build