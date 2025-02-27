#
# SqlServer
FROM mcr.microsoft.com/mssql/server AS sqlserver

CMD /bin/bash /scripts/base-scripts/entrypoint.sh