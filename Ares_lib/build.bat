if exist src\ (rmdir /s /q src)
xcopy /s ..\Ares\src src\
timeout 3
cls
cls
dotnet build
timeout 7