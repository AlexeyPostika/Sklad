"..\DC Console.exe" -o76 -zXXXXXXXX -a10 -c643
@IF NOT ERRORLEVEL 0 echo %ERRORLEVEL%



@for /f "tokens=2 delims='" %%i in ('FINDSTR /C:"[27] = " "..\result.txt"') do @set termIDTemp=%%i
timeout 4
"..\DC Console.exe" -o76 -z%termIDTemp% -s3600000
echo %ERRORLEVEL%

pause