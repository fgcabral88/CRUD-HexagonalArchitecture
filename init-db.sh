#!/bin/bash
# Aguarda o SQL Server iniciar
echo "Aguardando SQL Server iniciar..."
sleep 20

echo "Executando script de criação de banco..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'SuaSenhaForte123' -Q "
    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'financeiro_db_hexagonalarchitecture')
    BEGIN
        CREATE DATABASE financeiro_db_hexagonalarchitecture;
    END;

    USE financeiro_db_hexagonalarchitecture;

    IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'dev_felipe')
    BEGIN
        CREATE LOGIN dev_felipe WITH PASSWORD='dev2025', CHECK_POLICY=OFF;
    END;

    CREATE USER dev_felipe FOR LOGIN dev_felipe;
    EXEC sp_addrolemember 'db_owner', 'dev_felipe';
"
