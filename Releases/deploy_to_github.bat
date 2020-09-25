:: Copyright (c) Philipp Wagner. All rights reserved.
:: Licensed under the MIT license. See LICENSE file in the project root for full license information.

@echo off

echo ---------------------------------------------------
echo - Preparing the Github Release                    -
echo ---------------------------------------------------

:: Define the Executables, so we don't have to rely on pathes:
set DOTNET_EXECUTABLE="C:\Program Files\dotnet\dotnet.exe"

:: Logs to be used:
set STDOUT=stdout.log
set STDERR=stderr.log

:: Prompt for Sonatype:
set /p NUGET_PKG="NuGet Package: "

1>%STDOUT% 2>%STDERR% (

    echo Now publishing to Github. See stdout.log and stderr.log for details ...
    
    :: The GH_DEPLOYMENT_TOKEN is an Environment Variable, so we don't enter it by hand:
    %DOTNET_EXECUTABLE% nuget add source https://nuget.pkg.github.com/bytefish/index.json -n github -u bytefish -p %GH_DEPLOYMENT_TOKEN%
    %DOTNET_EXECUTABLE% nuget push "%NUGET_PKG%" --source "github"
  
    echo Finished publishing to Github.
)

pause
