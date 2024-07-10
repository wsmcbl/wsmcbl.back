#!/bin/bash

rm -rf bin publish || true
dotnet test
dotnet publish "src/wsmcbl.src.csproj" -c Release -o publish 