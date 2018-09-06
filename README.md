# consoler
helper to create console based .net core tools

## Testing application

1. install package with `dotnet tool install --global Dotnet.Csp --version 0.5.0-beta`

2. Test on any existing mvc website `dotnet csp add netcore.angular`
   This must add a nuget package (check .csproj file) and a folder into wwwroot/lib
