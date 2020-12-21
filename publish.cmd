REM Folders structure:
REM publish.cmd
REM repo
REM api
REM client
REM appsettings.json

chcp 1251
cd /d %~dp0

echo [%date%_%time::=.%] deploy started >> deploy.log

REM get current version
IF EXIST "version.txt" (SET /p CURRENT_VERSION=<version.txt) ELSE (SET CURRENT_VERSION=0)

cd repo

git pull >> deploy.log

REM get new version
IF EXIST "version.txt" (SET /p REPO_VERSION=<version.txt) ELSE (SET REPO_VERSION=0)

if "%CURRENT_VERSION%" == "%REPO_VERSION%" GOTO:end

SET BUILD_PREFIX=d0.
REM set /A BUILDMAIN+=1 TODO move to deploy script

set BUILD=%BUILD_PREFIX%%BUILDMAIN%

echo [%date%_%time::=.%] Start building version %BUILD%  >> deploy.log

cd client\mirclient

REM usage: volgakondi/zkzelen/zolkolud
call npm i
call npm run build.%1

cd ..\..\api\MiRAPI\

sc stop mirapi

dotnet publish -o ..\..\..\api -c Release -r win-x64 -f netcoreapp3.1 /p:PublishSingleFile=true --self-contained false

cd ..\..\..

echo %REPO_VERSION%>version.txt

xcopy repo\client\mirclient\dist\* client\ /O /X /E /H /K /F /Y

xcopy appsettings.json api\appsettings.json /Y

sc start mirapi

echo [%date%_%time::=.%] deploy version %BUILD% >> deploy.log

repo\cmail\cmail.exe -host:zkolosvolgodonsk@mail.ru:Aa061280@smtp.mail.ru -to:boykokv@yandex.ru -to:sambuk@ivtex.net -from:zkolosvolgodonsk@mail.ru -subject:"Deployed version %BUILD%" -body:"For instance %1 deployed version %BUILD%" -starttls

:end


