@echo off
echo Requesting administrative privileges...

:: Check if the script is already running with elevated privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Already running with administrative privileges. Executing main script.
    call ResetPrintSpooler.bat
) else (
    echo Not running with administrative privileges. Re-launching with elevation...
    powershell -Command "Start-Process '%~0' -Verb RunAs"
    exit /b
)

:: Rest of your ResetPrintSpooler.bat script goes here
@echo off
echo Stopping Spooler...
net stop spooler

echo Deleting PRINTERS folder...
del /q /f /s C:\Windows\System32\spool\PRINTERS\*

echo Starting Spooler...
net start spooler

echo Printer service restarted and print queue cleared.
