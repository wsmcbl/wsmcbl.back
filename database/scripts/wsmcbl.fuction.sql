-- Insert in debt history by tariff
CREATE OR REPLACE FUNCTION Accounting.insert_debt_history_by_new_tariff()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.debthistory(studentId, tariffId, isPaid, schoolyear)
    SELECT s.studentId, NEW.tariffId, false, NEW.schoolyear FROM Accounting.Student s
        INNER JOIN Secretary.Student sec ON s.studentId = sec.studentId
    WHERE sec.studentState = true;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_tariff
    AFTER INSERT ON Accounting.Tariff
    FOR EACH ROW
EXECUTE FUNCTION Accounting.insert_debt_history_by_new_tariff();





-- Insert in debt history by student
CREATE OR REPLACE FUNCTION Accounting.insert_debt_history_by_new_student()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.debthistory(studentId, tariffId, isPaid, schoolyear)
    SELECT NEW.studentId, t.tariffId, false, t.schoolyear FROM Accounting.tariff t
        INNER JOIN Secretary.Student sec ON new.studentId = sec.studentId
    WHERE t.schoolyear = sec.schoolyear;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_student
    AFTER INSERT ON Accounting.student
    FOR EACH ROW EXECUTE FUNCTION Accounting.insert_debt_history_by_new_student();





-- Update debt history by transactions
CREATE OR REPLACE FUNCTION Accounting.update_debt_history()
    RETURNS TRIGGER AS $$
BEGIN
    UPDATE Accounting.DebtHistory SET isPaid = true
    WHERE studentId = (SELECT studentId FROM Accounting.Transaction WHERE transactionId = NEW.transactionId)
      AND tariffId = NEW.tariffId;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_debt_history
    AFTER INSERT ON Accounting.Transaction_Tariff
    FOR EACH ROW EXECUTE FUNCTION Accounting.update_debt_history();



-- Generate accounting.transaction id
CREATE SEQUENCE accounting.transaction_id_seq START 1;

CREATE OR REPLACE FUNCTION Accounting.generate_transaction_id()
    RETURNS varchar(20) AS $$
DECLARE
    year_part CHAR(2);
    seq_part CHAR(6);
BEGIN
    year_part := TO_CHAR(NOW(), 'YY');

    seq_part := LPAD(NEXTVAL('accounting.transaction_id_seq')::TEXT, 6, '0');

    RETURN year_part || seq_part || 'tst';
END;
$$ LANGUAGE plpgsql;



-- Generate secretary.student id
CREATE SEQUENCE secretary.student_id_seq START 1;

CREATE OR REPLACE FUNCTION secretary.generate_student_id()
    RETURNS TRIGGER AS $$
DECLARE
    year_part TEXT;
    seq_part TEXT;
    initials_part TEXT;
BEGIN
    year_part := TO_CHAR(NOW(), 'YYYY');
    seq_part := LPAD(NEXTVAL('secretary.student_id_seq')::TEXT, 4, '0');
    initials_part := LOWER(SUBSTR(NEW.name, 1, 1) || SUBSTR(NEW.secondname, 1, 1) || SUBSTR(NEW.surname, 1, 1) || SUBSTR(NEW.secondsurname, 1, 1));
    NEW.studentid := year_part || '-' || seq_part || '-' || initials_part;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_student_id
    BEFORE INSERT ON secretary.student
    FOR EACH ROW EXECUTE FUNCTION secretary.generate_student_id();




-- Insert in schoolyear_student by new student
CREATE OR REPLACE FUNCTION secretary.insert_schoolyear_student_by_new_student()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO secretary.schoolyear_student(schoolyear, studentid) SELECT TO_CHAR(NOW(), 'YYYY'), NEW.studentid;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_tariff
    AFTER INSERT ON secretary.student
    FOR EACH ROW EXECUTE FUNCTION secretary.insert_schoolyear_student_by_new_student();