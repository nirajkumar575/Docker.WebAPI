#!/bin/bash
set -e

#!/bin/bash
# Enable HTTPS with self-signed certificate
dotnet Docker.WebAPI.dll --urls "http://+:80;https://+:443"


echo "Applying EF Core migrations..."
dotnet ef database update

echo "Starting Web API..."
exec dotnet Docker.WebAPI.dll
