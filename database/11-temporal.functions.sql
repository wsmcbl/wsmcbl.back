-- ##################### TEMPORAL ###################### --
CREATE OR REPLACE FUNCTION Accounting.CHANGE_STUDENT_DISCOUNT()
    RETURNS TRIGGER AS
$$
BEGIN
    WITH query_aux AS
             (SELECT d.studentid, d.tariffid, round(t.amount * (1 - disc.amount)) as subtotal, t.late as islate
              FROM accounting.debthistory d
                       JOIN accounting.tariff t ON t.tariffid = d.tariffid
                       JOIN accounting.discount disc ON disc.discountid = new.discountid
              WHERE d.studentid = new.studentid
                and t.typeid = 1)
    UPDATE accounting.debthistory
    SET subamount = q.subtotal,
        arrear    = case when q.islate then round(q.subtotal * 0.1) else 0.0 end
    FROM query_aux q
    WHERE debthistory.studentid = q.studentid
      AND debthistory.tariffid = q.tariffid;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER TRG_CHANGE_STUDENT_DISCOUNT
    AFTER UPDATE
    ON accounting.student
    FOR EACH ROW
EXECUTE FUNCTION Accounting.CHANGE_STUDENT_DISCOUNT();
-- ##################### TEMPORAL ###################### --







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