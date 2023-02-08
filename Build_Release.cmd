::Boilderplate
@ECHO OFF
SETLOCAL ENABLEEXTENSIONS

::name of this script
SET me=%~n0
::directory of script
SET parent=%~dp0

ECHO %me%
::::::::::::::

SET msbuild="%parent%tools\msbuild.cmd"

IF NOT DEFINED cruisedesign_appcenterr_key_windows (
	ECHO "ERROR:::: cruisedesign_appcenterr_key_windows env variable not set"
	EXIT /B 1
)

IF NOT DEFINED build_config SET build_config="Release"

call %msbuild% %parent%\CruiseDesign\CruiseDesign.csproj -restore /target:Rebuild /p:Configuration=%build_config%;SolutionDir=%parent%\

::End Boilderplate
EXIT /B %errorlevel%