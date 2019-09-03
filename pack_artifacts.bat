@echo off
set configurationName=%1
IF "%configurationName%" == "" /p configurationName=Release ConfigurationName: %=%

call .\publish_win10.bat %configurationName%

robocopy .\MarathonManager\bin\%configurationName%\ .\Publish\MarathonManager\ /E