::@ECHO OFF
SETLOCAL ENABLEEXTENSIONS

::Boilderplate 
::detect if invoked via Window Explorer
SET interactive=1
ECHO %CMDCMDLINE% | FIND /I "/c" >NUL 2>&1
IF %ERRORLEVEL% == 0 SET interactive=0

::name of this script
SET me=%~n0
::directory of script
SET parent=%~dp0
SET zip="%parent%tools\zip.cmd"

IF NOT DEFINED dateCode (SET dateCode=%date:~10,4%_%date:~4,2%_%date:~7,2%)
IF NOT DEFINED artifactsDir (SET artifactsDir=%parent%Artifacts\%dateCode%\)

set outFile=%artifactsDir%CruiseDesign.zip

cd %parent%CruiseDesign\bin\Release\net472

call %zip% a -tzip -spf %outFile%  CruiseDesign.exe CruiseDesign.exe.config runtimes\win-x86\native\*.dll runtimes\win-x64\native\*.dll *.dll

::if invoked from windows explorer, pause
IF "%interactive%"=="0" PAUSE
ENDLOCAL
EXIT /B 0