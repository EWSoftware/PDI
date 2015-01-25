@ECHO OFF
CLS

"%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m /m "Source\EWSPDI.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

"%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m /m "Demos\CSharpDemos\CSharpDemos.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

"%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m /m "Demos\VBNetDemos\VBNetDemos.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

"%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m /m "Demos\PDIWebDemo.sln" /t:Clean;Build

IF NOT "%SHFBROOT%"=="" "%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m "Doc\EWSoftwarePDI.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

IF "%SHFBROOT%"=="" ECHO **** Sandcastle help file builder not installed.  Skipping help build. ****

CD .\NuGet

..\Demos\.nuget\NuGet Pack EWSoftware.PDI.nuspec -NoPackageAnalysis -OutputDirectory ..\Deployment
..\Demos\.nuget\NuGet Pack EWSoftware.PDI.Data.nuspec -NoPackageAnalysis -OutputDirectory ..\Deployment
..\Demos\.nuget\NuGet Pack EWSoftware.PDI.Web.Controls.nuspec -NoPackageAnalysis -OutputDirectory ..\Deployment
..\Demos\.nuget\NuGet Pack EWSoftware.PDI.Windows.Forms.nuspec -NoPackageAnalysis -OutputDirectory ..\Deployment

CD ..
