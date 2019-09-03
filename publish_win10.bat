@echo off
set configurationName=%1
IF "%configurationName%" == "" /p configurationName=Release ConfigurationName: %=%

cd UI.JsonImport
dotnet publish -c %configurationName% -r win10-x64
robocopy .\bin\%configurationName%\netcoreapp2.2\win10-x64\ ..\Publish\UI.JsonImport\ /E

cd ..

cd UI.StartRuns
dotnet publish -c %configurationName% -r win10-x64
robocopy .\bin\%configurationName%\netcoreapp2.2\win10-x64\ ..\Publish\UI.StartRuns\ /E

cd ..

cd UI.TimeRecord
dotnet publish -c %configurationName% -r win10-x64
robocopy .\bin\%configurationName%\netcoreapp2.2\win10-x64\ ..\Publish\UI.TimeRecord\ /E

cd ..

cd UI.ExportResults
dotnet publish -c %configurationName% -r win10-x64
robocopy .\bin\%configurationName%\netcoreapp2.2\win10-x64\ ..\Publish\UI.ExportResults\ /E

cd ..