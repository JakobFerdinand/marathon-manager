param (
    [string]$configuration = "Release"
)

Write-Output $configuration

.\publish_win10.ps1 $configuration

robocopy (".\MarathonManager\bin\" + $configuration + "\") (".\Publish\" + $configuration + "\MarathonManager\") /E