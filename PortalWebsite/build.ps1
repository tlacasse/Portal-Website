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



<#$iconPath = Join-Path $buildPath 'favicon.ico'
if (Path-NotExists $iconPath){
	Copy-Item (Join-Path $PSScriptRoot 'favicon.ico') $iconPath -Verbose
}#>

#& (Resolve-Path 'C:\Users\**\AppData\Roaming\npm\gulp.cmd') --env $env

<#
$base = 'C:\Users\XYZ\Documents\GitHub\Portal\Portal'
$path = $base + '\_Build'
if (-not (Test-Path ($path))){
	New-Item -Path $path -ItemType directory
}
if (-not (Test-Path ($path + '\Scripts'))){
	New-Item -Path ($path + '\Scripts') -ItemType directory
}
if (-not (Test-Path ($path + '\Portal'))){
	New-Item -Path ($path + '\Portal') -ItemType directory
}
if (-not (Test-Path ($path + '\favicon.ico'))){
	Copy-Item ($base + '\favicon.ico') $path
}

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Removing old files.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green
$files = Get-ChildItem -Path $path -Recurse -exclude favicon.ico
$fileToDelete = @()
foreach($file in $files){
	$ignore = $false
	$ignore = $ignore -or ($file.FullName -like ($path + '\Portal*'))
	$ignore = $ignore -or ($file.FullName -like ($path + '\Scripts'))
	$ignore = $ignore -or ($file.FullName -like ($path + '\bin*'))
	$ignore = $ignore -or ($file.FullName -like ($path + '*.config'))
	$ignore = $ignore -or ($file.FullName -like ($path + '*.asax*'))
	if (-not $ignore){
		$fileToDelete += $file
	}
}
$fileToDelete | Select -ExpandProperty FullName | sort length -Descending | Remove-Item -Verbose

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Building CSproject.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green
$project = $base + '\Portal.csproj'
$msBuildExe = 'C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe'
& "$($msBuildExe)" "$($project)" /t:Build /m

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Gulp build.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green
& "C:\Users\XYZ\AppData\Roaming\npm\gulp.cmd" --env $env

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host '===FINISHED BUILD===' -ForegroundColor Green

$readVersion = [IO.File]::ReadAllText($base + '\_Build\Portal\version.txt')
$split = $readVersion.split('.')
$writeVersion = $split[0] + '.' + (([int]$split[1])+1)
$writeVersion | Out-File ($base + '\_Build\Portal\version.txt')
Write-Host ('= ' + $writeVersion) -ForegroundColor Green
#>