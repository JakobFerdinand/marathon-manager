$configuration = $env:CONFIGURATION

if ($configuration -eq $null) {
	$configuration = "Release"
}

Write-Output $configuration

.\publish_win10.ps1 $configuration

robocopy (".\MarathonManager\bin\" + $configuration + "\") (".\Publish\" + $configuration + "\MarathonManager\") /E