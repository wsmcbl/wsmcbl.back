-- Insert registration debt in debt history by student
CREATE OR REPLACE FUNCTION Accounting.insert_registration_debt_history_by_new_student()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
    SELECT NEW.studentId, t.tariffId, t.schoolyear, t.amount, 0.0, 0, false
    FROM Accounting.tariff t
             INNER JOIN Secretary.Student sec ON sec.studentId = new.studentId
    WHERE t.schoolyear = sec.schoolyear and t.typeid = 1;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_student AFTER INSERT ON Accounting.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.insert_registration_debt_history_by_new_student();


-- Update debt history by transactions --
CREATE OR REPLACE FUNCTION Accounting.update_debt_history()
    RETURNS TRIGGER AS $$
BEGIN
    UPDATE Accounting.DebtHistory dh
    SET debtbalance = dh.debtbalance + NEW.amount,
        isPaid = (dh.debtbalance + NEW.amount >= amount)
    FROM Accounting.Transaction t
    WHERE t.transactionId = NEW.transactionId
      AND t.studentId = dh.studentId
      AND dh.tariffId = NEW.tariffId;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_debt_history AFTER INSERT ON Accounting.Transaction_Tariff
    FOR EACH ROW EXECUTE FUNCTION Accounting.update_debt_history();


-- Update debt history by tariff overdue--
CREATE OR REPLACE FUNCTION Accounting.update_debt_history_by_tariff_overdue()
    RETURNS TRIGGER AS $$
BEGIN
    WITH query_aux AS
             (SELECT s.studentid, d.tariffid, round(new.amount*(1 - disc.amount)) as subtotal, new.late as islate
              FROM accounting.debthistory d
                       JOIN accounting.student s on s.studentid = d.studentid
                       JOIN accounting.discount disc ON disc.discountid = s.discountid
              WHERE d.tariffid = new.tariffid and new.typeid = 1)

    UPDATE Accounting.DebtHistory dh
    SET subamount = q.subtotal,
        arrear = case when q.islate then round(q.subtotal*0.1) else 0.0 end
    FROM query_aux as q
    where dh.studentid = q.studentid and
        dh.tariffid = q.tariffid;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_debt_history_by_tariff_overdue AFTER update ON Accounting.tariff
    FOR EACH ROW EXECUTE FUNCTION Accounting.update_debt_history_by_tariff_overdue();



-- Update ispaid in debt history--
CREATE OR REPLACE FUNCTION Accounting.update_ispaid_debt_history()
    RETURNS TRIGGER AS $$
BEGIN
    IF NEW.debtbalance >= (new.subamount + new.arrear) THEN
        NEW.ispaid := TRUE;
    ELSE
        NEW.ispaid := FALSE;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_debt_history
    BEFORE UPDATE ON Accounting.DebtHistory
    FOR EACH ROW EXECUTE FUNCTION Accounting.update_ispaid_debt_history();
