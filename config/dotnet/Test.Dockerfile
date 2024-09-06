FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY src/*.csproj ./src/
COPY tests/*.csproj ./tests/
COPY Makefile ./

VOLUME /root/.nuget/packages

RUN apt-get update && apt-get install -y make \
    && dotnet tool install --global dotnet-sonarscanner \
    && dotnet tool install --global dotnet-coverage

ENV PATH="/root/.dotnet/tools:${PATH}"

RUN cd src && dotnet restore
RUN cd tests && dotnet restore

#CMD ["dotnet", "test"]
CMD SONAR_TOKEN=$SONAR_TOKEN make dn-ss