chcp 1251

echo off

echo %date%_%time::=.% - publish started >> deploy.log

cd repo
git pull >> deploy.log

REM sc create ceapi binPath= "g:\Projects\Private\competencyevaluation\publish\api\ceapi.exe" type= share start= auto

cd %~dp0

IF NOT EXIST "bkp" mkdir bkp

IF NOT EXIST "api.7z" GOTO:api

for %%a in (api.7z) do set oldapidata=%%~ta
for %%a in (repo\api.7z) do set newapidata=%%~ta

echo Old API data: %oldapidata% >> deploy.log
echo New API data %newapidata% >> deploy.log

if "%oldapidata%" == "%newapidata%" GOTO:clientstart

:api

echo %date%_%time::=.% api is ready to update >> deploy.log

"c:\Program Files\7-Zip\7z.exe" a bkp\api_%date%_%time::=.%.7z .\api\* -r

REM sqlcmd -S AERO5\SQLEXPRESS14 -U sa -P open -d ce -Q "BACKUP DATABASE [CompetencyEvaluation] TO  DISK = N'%~dp0bkp\CompetencyEvaluation_%date%_%time::=.%.bak' WITH NOFORMAT, NOINIT,  NAME = N'CompetencyEvaluation-Полная База данных Резервное копирование', SKIP, NOREWIND, NOUNLOAD,  STATS = 10" >> deploy.log

del api.7z /F /Q
copy repo\api.7z api.7z
sc stop mirapi

TIMEOUT 10

"c:\Program Files\7-Zip\7z.exe" x api.7z -oapi -y

sc start mirapi

echo %date%_%time::=.% api updated >> deploy.log

:clientstart

IF NOT EXIST "client.7z" GOTO:client

for %%a in (client.7z) do set oldclientdata=%%~ta
for %%a in (repo\client.7z) do set newclientdata=%%~ta

echo Old API data: %oldclientdata% >> deploy.log
echo New API data %newclientdata% >> deploy.log

if "%oldclientdata%" == "%newclientdata%" GOTO:exit

:client

echo %date%_%time::=.% client is ready to update >> deploy.log

"c:\Program Files\7-Zip\7z.exe" a bkp\client_%date%_%time::=.%.7z .\client\* -r
del client\* /F /Q

copy repo\client.7z client.7z

"c:\Program Files\7-Zip\7z.exe" x client.7z -oclient -y

echo %date%_%time::=.% client updated >> deploy.log

:exit
IF NOT "%oldclientdata%" == "%newclientdata%" IF NOT "%oldapidata%" == "%newapidata%") SendSMTP_v2.19.0.1\SendSMTP.exe /Host smtp.gmail.com /Port 587 /Auth 2 /UserID boyko.constantine@gmail.com /PASS3 4v1i/NE61SQbgAtbMNDhWSlcFMsN/WneUe7/1gZ5dYEKOMEjvEj57lAoWUcJqhKd /From boyko.constantine@gmail.com /To boykokv@yandex.ru;account@ivtex.net;Lotos.17@mail.ru /subject "MIR Volga Выложена новая версия от %date%" /body "Залита новая версия. Необходимо проверить" /log

echo %date%_%time::=.% - All done >> deploy.log