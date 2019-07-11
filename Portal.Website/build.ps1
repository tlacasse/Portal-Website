param (
      [string]$env = 'dev'
    , [string]$buildPath = (Join-Path $PSScriptRoot '_Build')
    , [switch]$all
    , [switch]$portal
    , [switch]$server
    , [switch]$framework
    , [switch]$config
    , [string]$connectionString = ('Data Source=' + (Join-Path $buildPath 'App_Data\PortalWebsite.db') + ";Version=3;Password=portal;")
)

function Path-NotExists() {
    param (
        [string]$pathToTest
    )

    return -not (Test-Path($pathToTest))
} 

function Inject-WebConfig() {
    param (
          [string]$key
        , [string]$value
    )

    $configPath = Join-Path $buildPath 'Web.config'
    $webConfig = [System.IO.File]::ReadAllText($configPath)
    Write-Host ("Replace '#{" + $key + "}' with '$($value)'") -ForegroundColor 'magenta'
    $webConfig = $webConfig.Replace('#{' + $key + '}', $value)
    [System.IO.File]::WriteAllText($configPath, $webConfig)
}

Write-Host '-----------------------------' -ForegroundColor Green
Write-Host 'Checking File Existence.' -ForegroundColor Green
Write-Host '-----------------------------' -ForegroundColor Green

if (Path-NotExists $buildPath) {
    New-Item -Path $buildPath -ItemType directory -Verbose
}
$scriptsPath = Join-Path $buildPath 'Scripts'
if (Path-NotExists $scriptsPath) {
    New-Item -Path $scriptsPath -ItemType directory -Verbose
}
$portalPath = Join-Path $buildPath 'App_Data'
if (Path-NotExists $portalPath) {
    New-Item -Path $portalPath -ItemType directory -Verbose
}

if ($all -or $config) {
    robocopy $PSScriptRoot $buildPath 'favicon.ico' 'Web.config' 'Global.asax' /COPY:DATS
    Inject-WebConfig -Key 'ConnectionString' -Value $connectionString
}

robocopy (Join-Path $PSScriptRoot 'Views') (Join-Path $buildPath 'Views') /COPY:DATS /S

if ($all -or $server) {
    Write-Host '-----------------------------' -ForegroundColor Green
    Write-Host 'Building CSproject.' -ForegroundColor Green
    Write-Host '-----------------------------' -ForegroundColor Green

    $project = Join-Path $PSScriptRoot 'PortalWebsite.csproj'
    $msBuildExe = Resolve-Path 'C:\Program Files (x86)\MSBuild\**\Bin\msbuild.exe'
    if ($env -eq 'release') {
        & $msBuildExe $project /t:Build /m /p:Configuration=Release
    } else {
        & $msBuildExe $project /t:Build /m
    }

    Write-Host '-----------------------------' -ForegroundColor Green
    Write-Host 'Copy Binaries.' -ForegroundColor Green
    Write-Host '-----------------------------' -ForegroundColor Green

    $sourceBin = Join-Path $PSScriptRoot 'bin'
    $destBin = Join-Path $buildPath 'bin'
    robocopy $sourceBin $destBin /COPY:DATS /S

    if ($env -eq 'release') {
        Get-ChildItem (Join-Path $buildPath 'bin') | 
            Where-Object { $_.FullName -like '*.xml' -or $_.FullName -like '*.pdb'} | 
            Remove-Item -Verbose
    }
}

if ($all -or (-not $server)) {
    Write-Host '-----------------------------' -ForegroundColor Green
    Write-Host 'Gulp build.' -ForegroundColor Green
    Write-Host '-----------------------------' -ForegroundColor Green

    $gulp = Resolve-Path 'C:\Users\**\AppData\Roaming\npm\gulp.cmd'
    
    $tasks = @()
    if ($all -or $framework) {
        $tasks += 'Framework'
    }
    if ($all -or $portal){
        $tasks += 'Shared'
    }
    if ($all -or $portal) {
        $tasks += 'Portal'
    }
    & $gulp 'build' --env $env --what ($tasks -join ',')
}
