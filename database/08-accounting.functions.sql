-- Insert registration-debt in debt history by student --
CREATE OR REPLACE FUNCTION Accounting.insert_registration_debt_history_by_new_student()
    RETURNS TRIGGER AS $$
DECLARE
    current_school_year varchar(20);
    new_school_year varchar(20):= '';
    current_month INTEGER;
    current_year INTEGER;
BEGIN
    current_month := EXTRACT(MONTH FROM CURRENT_DATE);
    current_year := EXTRACT(YEAR FROM CURRENT_DATE);
    
    SELECT schoolyearid INTO current_school_year
    FROM secretary.schoolyear
    WHERE label = CAST(current_year AS VARCHAR);

    IF current_month IN (11, 12) THEN
        current_year := current_year + 1;

        SELECT schoolyearid INTO new_school_year
        FROM secretary.schoolyear
        WHERE label = CAST(current_year AS VARCHAR);
    END IF;
    
    INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
    SELECT NEW.studentId, t.tariffId, t.schoolyear, t.amount, 0.0, 0, false
    FROM Accounting.tariff t
    WHERE t.schoolyear = current_school_year or t.schoolyear = new_school_year
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


-- Update debt history by pay registration-debt --
create function Accounting.update_debt_history_by_enroll_student() returns trigger
    language plpgsql
as $$
DECLARE
    t_type             INTEGER;
    t_ispaid           BOOLEAN;
    s_educationallevel smallint;
    school_year_id     VARCHAR;
    s_discount         DOUBLE PRECISION;
BEGIN
    SELECT t.typeid, debt.ispaid, s.educationallevel, d.amount, en.schoolyear
    INTO t_type, t_ispaid, s_educationallevel, s_discount, school_year_id
    FROM accounting.debthistory as debt
             JOIN Accounting.tariff AS t ON t.tariffid = debt.tariffid
             JOIN accounting.student as s on s.studentid = debt.studentid
             JOIN accounting.discounteducationallevel as d on d.del = s.discountel
             JOIN academy.enrollment as en on en.enrollmentid = new.enrollmentid
    where debt.studentid = NEW.studentid;

    IF (t_type = 2 AND t_ispaid) THEN
        INSERT INTO Accounting.debthistory(studentId, tariffId, schoolyear, subamount, arrear, debtbalance, ispaid)
        SELECT NEW.studentId,
               t.tariffId,
               t.schoolyear,
               case when t.typeid = 1 then t.amount * (1 - s_discount) else t.amount end,
               case when t.late then t.amount * (1 - s_discount) * 0.1 else 0.0 end,
               0,
               false
        FROM Accounting.tariff t
        WHERE t.schoolyear = school_year_id
          AND t.typeid != 2
          AND t.educationallevel = s_educationallevel;
    END IF;

    RETURN NEW;
END;
$$;

CREATE TRIGGER trg_update_debt_history_by_enroll_student AFTER insert ON academy.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.update_debt_history_by_enroll_student();
    

-- Update debt history by tariff overdue --
CREATE OR REPLACE FUNCTION Accounting.update_debt_history_by_tariff_overdue()
    RETURNS TRIGGER AS $$
BEGIN
    WITH query_aux AS
             (SELECT s.studentid, d.tariffid, round(new.amount*(1 - disc.amount)) as subtotal, new.late as islate
              FROM accounting.debthistory d
                       JOIN accounting.student s on s.studentid = d.studentid
                       JOIN accounting.DiscountEducationalLevel disc ON disc.del = s.discountel
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



-- Update ispaid in debt history --
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
