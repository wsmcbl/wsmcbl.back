#!/bin/bash

rm -rf bin publish || true
dotnet test --verbosity detailed
dotnet publish "src/wsmcbl.src.csproj" -c Release -o publish 