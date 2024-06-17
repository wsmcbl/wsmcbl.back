#!/bin/bash

rm -rf bin publish || true
dotnet publish "wsmcbl.src.csproj" -c Release -o publish