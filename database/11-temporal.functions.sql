-- ##################### TEMPORAL ###################### --
-- ##### DELETE THIS FUNCTION IN PRODUCTION DEPLOY ##### --
CREATE OR REPLACE FUNCTION cleanDatabase() RETURNS void AS
$$
DECLARE
    schemas_name   TEXT;
    tables_name    TEXT;
    sequences_name TEXT;
BEGIN
    FOR schemas_name IN SELECT schema_name
                        FROM information_schema.schemata
                        WHERE schema_name IN ('academy', 'accounting', 'secretary')
        LOOP
            FOR tables_name IN SELECT table_name
                               FROM information_schema.tables
                               WHERE tables.table_schema = schemas_name
                                 AND tables.table_type = 'BASE TABLE'
                LOOP
                    EXECUTE 'TRUNCATE TABLE ' || schemas_name || '.' || tables_name || ' CASCADE;';
                END LOOP;
        END LOOP;

    FOR schemas_name IN SELECT schema_name
                        FROM information_schema.schemata
                        WHERE schema_name IN ('academy', 'accounting', 'secretary')
        LOOP
            FOR sequences_name IN SELECT sequence_name
                                  FROM information_schema.sequences
                                  WHERE sequences.sequence_schema = schemas_name
                LOOP
                    EXECUTE 'ALTER SEQUENCE ' || schemas_name || '.' || sequences_name || ' RESTART WITH 1;';
                END LOOP;
        END LOOP;
END;
$$ LANGUAGE plpgsql;
-- ##################### TEMPORAL ###################### --