-- drop database api_bd_test;
-- create database api_bd_test;

drop database if exists wsmcbl_database;
create database wsmcbl_database;

-- **Sequence to create**
-- config
-- secretary generate_id
-- secretary
-- academy
-- accounting
    
-- functions
-- temporal functions
-- data


-- ##################### TEMPORAL ###################### --
CREATE OR REPLACE FUNCTION Accounting.INSERT_STUDENT_ACCOUNTING()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.student(studentid, discountid)
    SELECT NEW.studentId, '1';

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER TRG_INSERT_STUDENT_ACCOUNTING
    AFTER INSERT ON secretary.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.INSERT_STUDENT_ACCOUNTING();
-- ##################### TEMPORAL ###################### --



-- ##################### TEMPORAL ###################### --
CREATE OR REPLACE FUNCTION Accounting.CHANGE_STUDENT_DISCOUNT()
    RETURNS TRIGGER AS $$
BEGIN
    WITH query_aux AS
             (SELECT d.studentid, d.tariffid, round(t.amount*(1 - disc.amount)) as subtotal, t.late as islate
              FROM accounting.debthistory d
                       JOIN accounting.tariff t ON t.tariffid = d.tariffid
                       JOIN accounting.discount disc ON disc.discountid = new.discountid
              WHERE d.studentid = new.studentid and t.typeid = 1)
    UPDATE accounting.debthistory
    SET subamount = q.subtotal,
        arrear = case when q.islate then round(q.subtotal*0.1) else 0.0 end
    FROM query_aux q WHERE debthistory.studentid = q.studentid AND debthistory.tariffid = q.tariffid;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER TRG_CHANGE_STUDENT_DISCOUNT
    AFTER UPDATE ON accounting.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.CHANGE_STUDENT_DISCOUNT();
-- ##################### TEMPORAL ###################### --
