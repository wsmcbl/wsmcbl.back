#!/bin/bash

export PGPASSWORD=pass12345

echo "Esperando BD..."
until psql -h database -U user -d wsmcbl_database -c '\q'; do
    echo "Esperando BD..."
    sleep 1
done
echo "BD lista."

dotnet ef database update

exec "$@"
