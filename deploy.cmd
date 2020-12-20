chcp 1251
cd /d %~dp0
git log --all --pretty=format:"%ad %s" --date=short --grep="^[^(i)]" > api\MiRAPI\changes.txt

REM version prefix
SET BUILD_PREFIX=d0.

IF EXIST "version.txt" (SET /p BUILDMAIN=<version.txt) ELSE (SET BUILDMAIN=0)

set /A BUILDMAIN+=1

set BUILD=%BUILD_PREFIX%%BUILDMAIN%

echo %BUILD%

echo export const BUILD_VERSION = '%BUILD%' > client\mirclient\src\version.js
echo export const BUILD_DATE = '%date%' >> client\mirclient\src\version.js

echo namespace MiRAPI { public static class Version { public const string BUILD_VERSION = "%BUILD%"; public const string BUILD_DATE = "%date%";}} > api\MiRAPI\version.cs

echo %BUILDMAIN% > version.txt

git add api\MiRAPI\changes.txt version.txt client\mirclient\src\version.js api\MiRAPI\version.cs
git commit -m "(i) deployment"
git push
git stash
git checkout release
git rebase master
git push --force
git switch -
git stash pop
