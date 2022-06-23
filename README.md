# FormationPlus

In this Repo, you can find the source code of this windows form application with database files and a .exe file that is ready to run.

To connect the app to the database files, follow these instructions:

- Install [Microsoft SQL Server Managment Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

- Install [Microsoft SQL Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

- Copy or cut and paste both the .mdf and .ldf database files in to the MS SQL Server path location on your system: ThisPC/C:/Program Files/Microsoft SQL Server/yourSQLExpressServer/MSSQL/DATA

- Open SQL Server Managment Studio, it will suggest a connection to the SQL Express Server, click connect

- Once opened, right click on Databases folder in the Object Explorer and select Attach option

- When you clicked Attach, a window will open, In this just click on Add button, select the database .mdf and .ldf files and add it

- Refresh your databases, now you can access the FormationPlus DB

- Double click the Test Softia.exe to run it