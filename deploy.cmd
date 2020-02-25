chcp 1251
cd /d %~dp0
git log --all --pretty=format:"%%ad %%s" --date=short > api\MiRAPI\changes.txt

REM version prefix
SET BUILD_PREFIX=d0.

IF EXIST "version.txt" (SET /p BUILDMAIN=<version.txt) ELSE (SET BUILDMAIN=0)

set /A BUILDMAIN+=1

set BUILD=%BUILD_PREFIX%%BUILDMAIN%

echo %BUILD%

echo export const BUILD_VERSION = '%BUILD%' > client\mirclient\src\version.js
echo export const BUILD_DATE = '%date%' >> client\mirclient\src\version.js

echo namespace ceapi { public static class Version { public const string BUILD_VERSION = "%BUILD%"; public const string BUILD_DATE = "%date%";}} > api\MiRAPI\version.cs

del api.7z /F
del client.7z /F

cd api\MiRAPI\
dotnet publish -o ..\..\publish\api -c Release -r win-x64 -f netcoreapp3.1 /p:PublishSingleFile=true --self-contained false

cd ..\..

cd client\mirclient

call npm run build

cd ..\..

xcopy client\mirclient\dist\* publish\client\ /O /X /E /H /K /F /Y

copy web.config publish\client\web.config /Y

echo %BUILDMAIN% > version.txt

"c:\Program Files\7-Zip\7z.exe" a api.7z .\publish\api\* -r
"c:\Program Files\7-Zip\7z.exe" a client.7z .\publish\client\* -r

git add changes.txt version.txt api.7z client.7z client\mirclient\src\version.js api\MiRAPI\version.cs
git commit -m "(i) deployment"
git push
REM hg commit changes.txt version.txt api.7z client.7z client\mirclient\src\version.js srv\ceapi\mirclient\version.cs -m "(i) deployment"
REM hg push