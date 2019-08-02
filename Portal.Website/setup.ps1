param (
      [string]$buildName = '_Build'
    , [string]$buildPath = (Join-Path $PSScriptRoot $buildName)
)

# gulp install

$packages = @(
    'gulp'
    'gulp-clean-css'
    'gulp-concat'
    'gulp-inject'
    'gulp-minify'
    'gulp-noop'
    'gulp-rename'
    'gulp-sass'
    'gulp-sort'
    'minimist'
    'del'
)

$packages | % { npm install $_ --save-dev }

# Path

New-Item -ItemType Directory -Path $buildPath
New-Item -ItemType Directory -Path (Join-Path $buildPath 'App_Data')
New-Item -ItemType Directory -Path (Join-Path $buildPath 'Data')
New-Item -ItemType Directory -Path (Join-Path $buildPath 'Data/Icons')
Copy-Item (Join-Path $PSScriptRoot '-1.png') -Destination (Join-Path $buildPath 'App_Data/Icons')

# Notes
Write-Host
Write-Host "Create database in $buildName/App_Data" -ForegroundColor 'green'
