@ECHO OFF
CLS

RD /S /Q .\Deployment

MD .\Deployment

DEL Source\EWSPDI\bin\Release\*.nupkg
DEL Source\EWSPDIData\bin\Release\*.nupkg
DEL Source\EWSPDIWeb\bin\Release\*.nupkg
DEL Source\EWSPDIWinForms\bin\Release\*.nupkg

IF EXIST "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current" SET "MSBUILD=%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\bin\MSBuild.exe"
IF EXIST "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current" SET "MSBUILD=%ProgramFiles%\Microsoft Visual Studio\2022\Professional\MSBuild\Current\bin\MSBuild.exe"
IF EXIST "%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current" SET "MSBUILD=%ProgramFiles%\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\bin\MSBuild.exe"

"%MSBUILD%" /nologo /v:m /m Source\EWSPDI.sln /t:Clean;Restore;Build "/p:Configuration=Release;Platform=Any CPU"

COPY Source\EWSPDI\bin\Release\*.nupkg .\Deployment
COPY Source\EWSPDIData\bin\Release\*.nupkg .\Deployment
COPY Source\EWSPDIWeb\bin\Release\*.nupkg .\Deployment
COPY Source\EWSPDIWinForms\bin\Release\*.nupkg .\Deployment

IF NOT "%SHFBROOT%"=="" "%MSBUILD%" /nologo /v:m Doc\EWSoftwarePDI.sln /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

IF "%SHFBROOT%"=="" ECHO **** Sandcastle help file builder not installed.  Skipping help build. ****
