@echo off
setlocal enabledelayedexpansion

set "defaultAppPath=C:\Program Files\WindowsApps\Microsoft.Windows.Photos_2023.11110.29003.0_x64__8wekyb3d8bbwe\PhotosApp.exe"
set "photoExtensions=.jpg;.jpeg;.png"
set "defaultAppValue=PhotoViewer.FileAssoc.JFIF"

for %%x in (%photoExtensions%) do (
    set "regPath=HKCU\Software\Classes\%%x"
    reg add "!regPath!" /f >nul 2>&1
    reg add "!regPath!" /v "(Default)" /t REG_SZ /d "%defaultAppValue%" /f >nul 2>&1
)

echo Photos App set as Default application for %photoExtensions% files.
endlocal

