@echo off
set PAUSE_ERRORS=1

set /p BUILD_NUMBER=<"version.txt"
echo %BUILD_NUMBER%

set /p BUILD_TYPE=<"buildType.txt"
echo %BUILD_TYPE%

"C:\Program Files\7-Zip\7z" a -tzip ../../zip/SuperLemonadeFactory1989.%BUILD_TYPE%.%BUILD_NUMBER%.zip * -r -x!Package.bat