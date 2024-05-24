#!/bin/bash

rm -rf bin publish || true
dotnet publish "wsmcbl.back.csproj" -c Release -o publish
sudo cp swagger.json /publish