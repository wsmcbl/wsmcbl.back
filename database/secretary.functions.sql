-- Generate secretary.student id
CREATE SEQUENCE if not exists secretary.student_id_seq START 1;

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

CREATE TRIGGER trg_generate_student_id BEFORE INSERT ON secretary.student
    FOR EACH ROW EXECUTE FUNCTION secretary.generate_student_id();


-- Insert in schoolyear_student by new student
CREATE OR REPLACE FUNCTION secretary.insert_schoolyear_student_by_new_student()
    RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO secretary.schoolyear_student(schoolyear, studentid)
    SELECT s.schoolyearid, NEW.studentid from secretary.schoolyear s where s.label = TO_CHAR(NOW(), 'YYYY');

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_debt_history_by_new_tariff AFTER INSERT ON secretary.student
    FOR EACH ROW EXECUTE FUNCTION secretary.insert_schoolyear_student_by_new_student();


-- Update degree by new enrollments --
CREATE OR REPLACE FUNCTION Secretary.update_degree()
    RETURNS TRIGGER AS $$
BEGIN
    UPDATE Secretary.Degree d
    SET quantity = d.quantity + 1
    FROM Secretary.Degree
    WHERE d.degreeid = NEW.degreeid;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_degree AFTER INSERT ON Academy.Enrollment
    FOR EACH ROW EXECUTE FUNCTION secretary.update_degree();