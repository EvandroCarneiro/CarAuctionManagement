#!/usr/bin/env bash

echo "#### Initializing database"

# create DB and run scripts
/scripts/base-scripts/setup_database.sh & 

# start SQL Server
/opt/mssql/bin/sqlservr