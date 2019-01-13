param (
    [string]$buildPath = (Join-Path $PSScriptRoot '_Build')
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
New-Item -ItemType Directory -Path (Join-Path $buildPath 'Portal')
New-Item -ItemType Directory -Path (Join-Path $buildPath 'Portal/Icons')
Copy-Item (Join-Path $PSScriptRoot '-1.png') -Destination (Join-Path $buildPath 'Portal/Icons')
