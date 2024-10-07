-- Insert registration debt in debt history by student
CREATE OR REPLACE FUNCTION Accounting.insert_registration_debt_history_by_new_student()
    RETURNS TRIGGER AS $$
DECLARE
    current_school_year varchar(20);
BEGIN
    SELECT schoolyearid INTO current_school_year
    FROM secretary.schoolyear
    WHERE label = to_char(current_date, 'YYYY');
    
    INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
    SELECT NEW.studentId, t.tariffId, t.schoolyear, t.amount, 0.0, 0, false
    FROM Accounting.tariff t
    WHERE t.schoolyear = current_school_year
      and t.typeid = 2
      and NEW.educationallevel = t.educationallevel;

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


-- Update debt history by pay registration debt --
CREATE OR REPLACE FUNCTION Accounting.update_debt_history_by_enroll_student()
    RETURNS TRIGGER AS $$
DECLARE 
    t_type INTEGER;
    t_ispaid BOOLEAN;
    s_educationallevel smallint;
    current_school_year VARCHAR;
    s_discount DOUBLE PRECISION;
BEGIN
    SELECT t.typeid, debt.ispaid, s.educationallevel, d.amount INTO t_type, t_ispaid, s_educationallevel, s_discount
    FROM accounting.debthistory as debt
        JOIN Accounting.tariff AS t ON t.tariffid = debt.tariffid
        JOIN accounting.student as s on s.studentid = debt.studentid
        JOIN accounting.discount as d on d.discountid = s.discountid
    where debt.studentid = NEW.studentid;

    SELECT schoolyearid INTO current_school_year
    FROM secretary.schoolyear
    WHERE label = to_char(current_date, 'YYYY');
    
    IF (t_type = 2 AND t_ispaid) THEN
        INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
        SELECT    
            NEW.studentId,
            t.tariffId,
            t.schoolyear,
            case when t.typeid = 1 then t.amount*(1 - s_discount) else t.amount end,
            case when t.late then t.amount*(1 - s_discount)*0.1 else 0.0 end,
            0,
            false
        FROM Accounting.tariff t
        WHERE t.schoolyear = current_school_year
          AND t.typeid != 2
          AND t.educationallevel = s_educationallevel;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_debt_history_by_enroll_student AFTER update ON academy.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.update_debt_history_by_enroll_student();
    

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
