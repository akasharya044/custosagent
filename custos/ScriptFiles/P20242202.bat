@echo off
setlocal enabledelayedexpansion

set "acrobatPath=C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe"
set "pdfExtension=.pdf"
set "regPath=HKCU\Software\Classes\%pdfExtension%"
set "defaultAppValue=AcroExch.Document.DC"

REM Create registry key
reg add "%regPath%" /f >nul 2>&1

REM Set default value for the registry key
reg add "%regPath%" /v "(Default)" /t REG_SZ /d "%defaultAppValue%" /f >nul 2>&1

echo Adobe Acrobat set as the default application for PDF files.

endlocal
