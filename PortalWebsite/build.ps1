param(
	  [string]$env = 'dev'
	, [string]$buildPath = (Join-Path $PSScriptRoot '_Build')
)

function Path-NotExists(){
	param(
		[string]$pathToTest
	)
	return -not (Test-Path($pathToTest))
} 

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Checking File Existence.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green

if (Path-NotExists $buildPath){
	New-Item -Path $buildPath -ItemType directory -Verbose
}
$scriptsPath = Join-Path $buildPath 'Scripts'
if (Path-NotExists $scriptsPath){
	New-Item -Path $scriptsPath -ItemType directory -Verbose
}
$portalPath = Join-Path $buildPath 'Portal'
if (Path-NotExists $portalPath){
	New-Item -Path $portalPath -ItemType directory -Verbose
}
robocopy $PSScriptRoot $buildPath 'favicon.ico' 'Web.config' 'index.html' /COPY:DATS

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Building CSproject.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green

$project = Join-Path $PSScriptRoot 'PortalWebsite.csproj'
$msBuildExe = Resolve-Path 'C:\Program Files (x86)\MSBuild\**\Bin\msbuild.exe'
& $msBuildExe $project /t:Build /m

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Copy Binaries.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green

$sourceBin = Join-Path $PSScriptRoot 'bin'
$destBin = Join-Path $buildPath 'bin'
robocopy $sourceBin $destBin /COPY:DATS

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Gulp build.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green

& (Resolve-Path 'C:\Users\**\AppData\Roaming\npm\gulp.cmd') --env $env

$number = [int]([Environment]::GetEnvironmentVariable('portal_build_number', 'User')) + 1
[Environment]::SetEnvironmentVariable('portal_build_number', "$number", 'User')

Write-Host ("Build Number: $number") -ForegroundColor 'magenta'
