-- ##################### TEMPORAL ###################### --
CREATE OR REPLACE FUNCTION Accounting.CHANGE_STUDENT_DISCOUNT() RETURNS TRIGGER language plpgsql as
$$
BEGIN
    WITH query_aux AS
             (SELECT d.studentid, d.tariffid, round(t.amount * (1 - disc.amount)) as subtotal, t.late as islate
              FROM accounting.debthistory d
                       JOIN accounting.tariff t ON t.tariffid = d.tariffid
                       JOIN accounting.discounteducationallevel disc ON disc.del = new.discountel
              WHERE d.studentid = new.studentid
                and t.typeid = 1 and d.ispaid = FALSE)
    
    UPDATE accounting.debthistory
    SET subamount = q.subtotal,
        arrear    = case when q.islate then round(q.subtotal * 0.1) else 0.0 end
    FROM query_aux q
    WHERE debthistory.studentid = q.studentid
      AND debthistory.tariffid = q.tariffid;

    RETURN NEW;
END;
$$;

CREATE TRIGGER TRG_CHANGE_STUDENT_DISCOUNT
    AFTER UPDATE
    ON accounting.student
    FOR EACH ROW
EXECUTE FUNCTION Accounting.CHANGE_STUDENT_DISCOUNT();
-- ##################### TEMPORAL ###################### --