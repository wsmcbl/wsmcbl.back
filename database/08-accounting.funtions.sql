-- Insert in debt history by tariff
CREATE OR REPLACE FUNCTION Accounting.insert_debt_history_by_new_tariff()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
    SELECT s.studentId,
           NEW.tariffId,
           NEW.schoolyear,
           case when new.typeid = 1 then NEW.amount*(1 - d.amount) else new.amount end,
           case when new.late then (new.amount*(1 - d.amount)*0.1) else 0 end,
           0,
           false
    FROM Accounting.Student s
             JOIN Accounting.discount d ON d.discountid = s.discountid
             INNER JOIN Secretary.Student sec ON s.studentId = sec.studentId
    WHERE sec.studentState = true;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_tariff AFTER INSERT ON Accounting.Tariff
    FOR EACH ROW EXECUTE FUNCTION Accounting.insert_debt_history_by_new_tariff();


-- Insert in debt history by student
CREATE OR REPLACE FUNCTION Accounting.insert_debt_history_by_new_student()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
    SELECT NEW.studentId,
           t.tariffId,
           t.schoolyear,
           case when t.typeid = 1 then t.amount*(1 - d.amount) else t.amount end,
           case when t.late then t.amount*(1 - d.amount)*0.1 else 0.0 end,
           0,
           false
    FROM Accounting.tariff t
             INNER JOIN Secretary.Student sec ON sec.studentId = new.studentId
             INNER JOIN Accounting.student s on s.studentid = sec.studentid
             JOIN accounting.discount d on d.discountid = s.discountid
    WHERE t.schoolyear = sec.schoolyear;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_student AFTER INSERT ON Accounting.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.insert_debt_history_by_new_student();


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
