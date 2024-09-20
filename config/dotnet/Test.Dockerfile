FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/*.csproj ./src/
COPY tests/*.csproj ./tests/
COPY Makefile ./

VOLUME /root/.nuget/packages

RUN apt-get update && apt-get install -y \
    libgdiplus \
    libxml2 \
    glibc-source \
    && rm -rf /var/lib/apt/lists/*

RUN dotnet tool install --global dotnet-coverage
ENV PATH="/root/.dotnet/tools:${PATH}"

CMD dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"