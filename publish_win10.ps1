param (
    [string]$configuration = "Release"
)

$projects = "UI.JsonImport", "UI.StartRuns", "UI.TimeRecord", "UI.ExportResults"
$publishPath = ".\Publish\" + $configuration + "\"

foreach ($project in $projects) {
    Set-Location $project
    dotnet publish -c $configuration -r win10-x64
    Set-Location ..

    $outputPath = $project + "\bin\"
    if (Test-Path ($outputPath + "Any CPU") -PathType Any) {
        $outputPath = $outputPath + "Any CPU\"
    }
    $outputPath = $outputPath + $configuration + "\netcoreapp2.2\win10-x64"

    Robocopy.exe $outputPath ($publishPath + $project) /E
}