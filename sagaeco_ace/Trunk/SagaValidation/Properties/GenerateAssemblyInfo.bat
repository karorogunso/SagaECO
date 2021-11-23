::-----------------------------
::** Initialize
@ECHO OFF
SETLOCAL ENABLEEXTENSIONS

REM Initialize Constants
SET TSVN_INFO_FILE=.\TSVN_INFO.tmp

REM Initialize script arguments

SET workDir=%1
SET template=%2
SET target=%3

REM Goto main entry
GOTO MAIN
::=============================


::-----------------------------
::** Main entry
:MAIN
pushd %workDir%
SET workDir=.\

REM 检查参数
IF %workDir%=="" GOTO ARGUMENT_ERROR
IF %template%=="" GOTO ARGUMENT_ERROR
IF %target%=="" GOTO ARGUMENT_ERROR

REM 查询注册表
reg query HKLM\SOFTWARE\TortoiseSVN /v Directory > %TSVN_INFO_FILE% 2>NUL
echo %TSVN_INFO_FILE%

REM 查找 TSVN 路径
FOR /F "tokens=*" %%i IN (%TSVN_INFO_FILE%) DO (
  ECHO %%i | find "Directory" > NUL
  IF %ERRORLEVEL% == 0 (
    ECHO %%i > %TSVN_INFO_FILE%
  )
)

SET /P TSVN_PATH= < %TSVN_INFO_FILE%
echo %TSVN_INFO_FILE%
SET TSVN_PATH=%TSVN_PATH:~17,-1%
echo %TSVN_PATH%
REM 调用 TSVN 替换模板
IF NOT %ERRORLEVEL% == 0 GOTO UNKNOW_ERROR
"%TSVN_PATH%\bin\SubWCRev.exe" %workDir% %template% %target% >NUL

IF %ERRORLEVEL% == 0 GOTO SUCESSED

SET TSVN_PATH=%TSVN_PATH:~6,-1%
"%TSVN_PATH%\bin\SubWCRev.exe" %workDir% %template% %target% >NUL

IF %ERRORLEVEL% == 0 GOTO SUCESSED
SET TSVN_PATH=C:\Program Files\TortoiseSVN\
"%TSVN_PATH%\bin\SubWCRev.exe" %workDir% %template% %target% >NUL


IF NOT %ERRORLEVEL% == 0 GOTO UNKNOW_ERROR
GOTO SUCESSED
::=============================


::-----------------------------
::** Error handlers

:ARGUMENT_ERROR
ECHO 传入的参数无效。
GOTO FAIL

:NOT_FOUND_TSVN
ECHO 查询TortoiseSVN 的安装信息失败。
GOTO FAIL

:UNKNOW_ERROR
ECHO 生成程序集信息出现未知错误。
:FAIL
::=============================

::-----------------------------
::** Program exit
:FAIL
DEL /Q %TSVN_INFO_FILE% 2>NUL
ECHO 生成程序集信息失败。
popd
EXIT 1

:SUCESSED
DEL /Q %TSVN_INFO_FILE% 2>NUL
ECHO 生成程序集信息成功。
popd
EXIT 0
::=============================
