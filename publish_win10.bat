set configurationName=%1
IF "%configurationName%" == "" /p configurationName=Release ConfigurationName: %=%

cd UI.JsonImport
dotnet publish -c %configurationName% -r win10-x64
set outputPath=.\bin\%configurationName%\netcoreapp2.2\win10-x64\
if exist ".\bin\Any Cpu\" (
    set outputPath=".\bin\Any Cpu\%configurationName%\netcoreapp2.2\win10-x64\"
)
robocopy "%outputPath%" ..\Publish\UI.JsonImport\ /E

cd ..

cd UI.StartRuns
dotnet publish -c %configurationName% -r win10-x64
set outputPath=.\bin\%configurationName%\netcoreapp2.2\win10-x64\
if exist ".\bin\Any Cpu\" ( 
    set outputPath=".\bin\AnyCpu\%configurationName%\netcoreapp2.2\win10-x64\"
)
robocopy "%outputPath%" ..\Publish\UI.StartRuns\ /E

cd ..

cd UI.TimeRecord
dotnet publish -c %configurationName% -r win10-x64
set outputPath=.\bin\%configurationName%\netcoreapp2.2\win10-x64\
if exist ".\bin\AnyCpu\" ( 
    set outputPath=".\bin\AnyCpu\%configurationName%\netcoreapp2.2\win10-x64\"
)
robocopy "%outputPath%" ..\Publish\UI.TimeRecord\ /E

cd ..

cd UI.ExportResults
dotnet publish -c %configurationName% -r win10-x64
set outputPath=.\bin\%configurationName%\netcoreapp2.2\win10-x64\
if exist ".\bin\AnyCpu\" ( 
    set outputPath=".\bin\AnyCpu\%configurationName%\netcoreapp2.2\win10-x64\"
)
robocopy "%outputPath%" ..\Publish\UI.ExportResults\ /E

cd ..