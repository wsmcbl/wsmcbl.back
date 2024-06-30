#!/bin/bash

rm -rf bin publish || true
dotnet test --no-build
dotnet publish "wsmcbl.src.csproj" -c Release -o publish 