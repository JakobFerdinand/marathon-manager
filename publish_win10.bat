cd UI.JsonImport
dotnet publish -c Release -r win10-x64
robocopy \bin\Release\netcoreapp2.1\win10-x64\ ..\Publish\UI.JsonImport\ /E

cd ..

cd UI.StartRuns
dotnet publish -c Release -r win10-x64
robocopy \bin\Release\netcoreapp2.1\win10-x64\ ..\Publish\UI.StartRuns\ /E

cd ..

cd UI.TimeRecord
dotnet publish -c Release -r win10-x64
robocopy \bin\Release\netcoreapp2.1\win10-x64\ ..\Publish\UI.TimeRecord\ /E

cd ..

cd UI.ExportResults
dotnet publish -c Release -r win10-x64
robocopy \bin\Release\netcoreapp2.1\win10-x64\ ..\Publish\UI.ExportResults\ /E

cd ..