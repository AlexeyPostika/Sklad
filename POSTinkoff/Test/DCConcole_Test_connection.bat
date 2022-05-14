"..\DC Console.exe" -o1 -z40000037 -a10 -c643
@IF NOT ERRORLEVEL 0 echo %ERRORLEVEL%



@for /f "tokens=2 delims='" %%i in ('FINDSTR /C:"[27] = " "..\result.txt"') do @set termIDTemp=%%i
timeout 4
"..\DC Console.exe" -o26 -z%termIDTemp% -a10 -c643
echo %ERRORLEVEL%

pause
