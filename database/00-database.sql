DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'wsmcbl_database') THEN
            PERFORM dblink_exec('dbname=postgres user=' || current_user, 'CREATE DATABASE wsmcbl_database');
        END IF;
    END
$$;

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

--drop database if exists wsmcbl_database;
--create database wsmcbl_database;

-- **Sequence to create**
-- config
-- secretary generate_id
-- secretary
-- academy generate_id
-- academy
-- accounting generate_id
-- accounting
    
-- functions
-- temporal functions
-- data.catalog
-- data