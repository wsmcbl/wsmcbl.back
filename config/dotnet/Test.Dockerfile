FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/*.csproj ./src/
COPY tests/*.csproj ./tests/
COPY Makefile ./
COPY resource/ resource/

VOLUME /root/.nuget/packages

RUN dotnet tool install --global dotnet-coverage
ENV PATH="/root/.dotnet/tools:${PATH}"

CMD dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
