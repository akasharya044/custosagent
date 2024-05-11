@echo off
setlocal

REM Reset Wi-Fi adapter
set "wifiAdapterName="
for /f "tokens=*" %%a in ('netsh interface show interface ^| find "Wi-Fi"') do (
    set "wifiAdapterName=%%a"
)
if defined wifiAdapterName (
    netsh interface set interface "!wifiAdapterName!" admin=disable
    timeout /t 5 /nobreak >nul
    netsh interface set interface "!wifiAdapterName!" admin=enable
) else (
    echo Wi-Fi adapter not found.
    goto :EndScript
)

REM Clear saved Wi-Fi keys
netsh wlan delete profile name="*"

echo Wi-Fi reset and saved keys cleared.

:EndScript
endlocal
