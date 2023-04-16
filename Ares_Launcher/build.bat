dotnet build
dotnet publish -r win-x64 -c Release --self-contained false /p:PublishSingleFile=true
start .\bin\Release\net7.0-windows\win-x64\publish