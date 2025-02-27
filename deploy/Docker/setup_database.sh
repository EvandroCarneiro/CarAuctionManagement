#!/usr/bin/env bash

sleep 15

echo "#### Creating database"
/opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -P "BCA_db_p4ssw0rd" -i /scripts/base-scripts/create-database.sql

for script in /scripts/bca-scripts/*.sql
do
	echo "#### Running $script"
	/opt/mssql-tools18/bin/sqlcmd -C -S localhost -U sa -P "BCA_db_p4ssw0rd" -i $script
done
echo "#### Done"