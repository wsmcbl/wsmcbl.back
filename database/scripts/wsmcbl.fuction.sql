CREATE OR REPLACE FUNCTION Accounting.insert_debt_history()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO Accounting.debthistory(studentId, tariffId, isPaid, schoolyear)
    SELECT s.studentId, NEW.tariffId, false, NEW.schoolyear
    FROM Accounting.Student s
             INNER JOIN Secretary.Student sec ON s.studentId = sec.studentId
    WHERE sec.studentState = true;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history
    AFTER INSERT ON Accounting.Tariff
    FOR EACH ROW
EXECUTE FUNCTION Accounting.insert_debt_history();


CREATE OR REPLACE FUNCTION Accounting.update_debt_history()
    RETURNS TRIGGER AS $$
BEGIN
    UPDATE Accounting.DebtHistory
    SET isPaid = true
    WHERE studentId = (SELECT studentId FROM Accounting.Transaction WHERE transactionId = NEW.transactionId)
      AND tariffId = NEW.tariffId;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Crear el trigger
CREATE TRIGGER trg_update_debt_history
    AFTER INSERT ON Accounting.Transaction_Tariff
    FOR EACH ROW
EXECUTE FUNCTION Accounting.update_debt_history();

