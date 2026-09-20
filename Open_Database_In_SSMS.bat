@echo off
title Open Uppercut Barber Shop CRM in SSMS
echo Opening SQL Server Management Studio connected to UppercutBarberShopCRM database...
start "" "D:\database\Common7\IDE\SSMS.exe" -S "(localdb)\MSSQLLocalDB" -d UppercutBarberShopCRM "%~dp0UppercutBarberShopCRM_Database.sql"
echo Done! SSMS has been launched.
