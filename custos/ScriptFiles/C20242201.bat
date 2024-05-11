@echo off
setlocal

:: Clear temporary files from the user's profile
del /q /s "%temp%\*.*"
del /q /s "%userprofile%\AppData\Local\Temp\*.*"

:: Clear temporary files from the Windows directory
del /q /s "%windir%\Temp\*.*"

:: Clear temporary internet files
del /q /s /f "%userprofile%\AppData\Local\Microsoft\Windows\INetCache\*.*"

:: Clear Windows update temporary files
del /q /s /f "%windir%\SoftwareDistribution\Download\*.*"

echo Temporary files deleted successfully.

