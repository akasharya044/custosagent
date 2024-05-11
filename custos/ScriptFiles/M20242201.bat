@echo off

echo Current Memory Consumption:
systeminfo | find "Total Physical Memory"
systeminfo | find "Available Physical Memory"

echo.
echo Clearing Standby List to free up memory...

(
  echo Set objWMI = GetObject^("winmgmts:{impersonationLevel=impersonate}!\\.\root\cimv2"^)
  echo Set colOS = objWMI.ExecQuery^("SELECT * FROM Win32_OperatingSystem"^)
  echo For Each objOS in colOS
  echo   objOS.EmptyWorkingSet()
  echo Next
) | cscript //NoLogo


echo Memory Consumption After Optimization:
systeminfo | find "Total Physical Memory"
systeminfo | find "Available Physical Memory"

echo.
echo Memory optimization complete.

