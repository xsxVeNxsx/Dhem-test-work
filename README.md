# Dhem-test-work
C# project with basic DB functional and parsing of local files for data insertion.

## Run parameters
* <b>-n</b> DateBase name. <b>Default:</b> dhem__
* <b>-h</b> DateBase hosting. <b>Default:</b> db4free.net
* <b>-u</b> DateBase user name. <b>Default:</b> dhem_admin
* <b>-pass</b> DateBase user password. <b>Default:</b> dhem_admin
* <b>-port</b> DateBase hosting port. <b>Default:</b> 3306
* <b>-d</b> Data directory. <b>Default:</b> data/
* <b>-clear</b> Clear Storage table insted of insert data.

## Application user example
Connect to 'db4free.net:3306' host by 'dhem_admin:dhem_admin' user:pass at table 'dhem__' and clear Storage table
> DHEM_TestWork.exe -u dhem_admin -h db4free.net -pass dhem_admin -n dhem__ -port 3306 -clear

Connect to 'localhost:3306' host by 'root' user without password, table 'dhem' and insert data from 'C:\DHEM_TestWork\data' directory
> DHEM_TestWork.exe -u root -h localhost -n dhem -port 3306 -d "C:\DHEM_TestWork\data"
