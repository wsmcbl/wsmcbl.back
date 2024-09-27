FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/*.csproj ./
RUN dotnet restore

COPY src/ ./
RUN dotnet publish -c Release -o out

FROM custom-base-image AS runtime
WORKDIR /app
COPY --from=build /app/out .

COPY resource/ resource/

ENTRYPOINT ["dotnet", "wsmcbl.src.dll"]