chcp 1251
cd /d %~dp0
hg log --template "#rev 1.{rev} Date: {date|shortdate}\r\n{desc}\r\n" > api\MiRAPI\changes.txt

SET BUILD_PREFIX=d0.

IF EXIST "version.txt" (SET /p BUILDMAIN=<version.txt) ELSE (SET BUILDMAIN=0)

set /A BUILDMAIN+=1

set BUILD=%BUILD_PREFIX%%BUILDMAIN%

echo %BUILD%

echo export const BUILD_VERSION = '%BUILD%' > client\ceweb\mirclient\version.js
echo export const BUILD_DATE = '%date%' >> client\ceweb\mirclient\version.js

echo namespace ceapi { public static class Version { public const string BUILD_VERSION = "%BUILD%"; public const string BUILD_DATE = "%date%";}} > api\MiRAPI\version.cs

del api.7z /F
REM del client.7z /F

cd api\MiRAPI\
dotnet publish -o ..\..\publish\api -c Release -r win-x64 -f netcoreapp3.1 /p:PublishSingleFile=true --self-contained false

cd ..\..

cd client\mirclient

REM call npm run build

cd ..\..

REM xcopy client\mirclient\dist\* publish\client\ /O /X /E /H /K /F /Y

copy web.config publish\client\web.config /Y

echo %BUILDMAIN% > version.txt

"c:\Program Files\7-Zip\7z.exe" a api.7z .\publish\api\* -r
REM "c:\Program Files\7-Zip\7z.exe" a client.7z .\publish\client\* -r

REM hg commit changes.txt version.txt api.7z client.7z client\mirclient\src\version.js srv\ceapi\mirclient\version.cs -m "(i) deployment"
REM hg push