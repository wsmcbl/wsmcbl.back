-- Insert subject_partial by property gradeRecordIsActive true of partial updated --
CREATE OR REPLACE FUNCTION academy.insert_subject_partial_records_by_partial_updated()
    RETURNS TRIGGER LANGUAGE plpgsql AS
$$
DECLARE
    current_school_year VARCHAR;
BEGIN
    IF (new.isactive = 'true' and new.graderecordisactive = 'true') THEN
        SELECT schoolyearid
        INTO current_school_year
        FROM secretary.schoolyear
        WHERE label = to_char(current_date, 'YYYY');

        WITH query_aux AS
        (SELECT s.* FROM academy.subject s
            JOIN academy.enrollment e ON s.enrollmentid = e.enrollmentid
            JOIN secretary.subject ss ON ss.subjectid = s.subjectid
            LEFT JOIN academy.subject_partial sp ON sp.subjectid = s.subjectid
                AND sp.enrollmentid = s.enrollmentid
                AND sp.partialid = new.partialid
                AND s.teacherid = sp.teacherid
        WHERE e.schoolyear = current_school_year
          AND (ss.semester = 3 OR ss.semester = NEW.semester)
          AND sp.subjectpartialid is null)

        INSERT INTO academy.subject_partial(subjectid, enrollmentid, partialid, teacherid)
        SELECT qa.subjectid, qa.enrollmentid, new.partialid, qa.teacherid
        FROM query_aux qa;

    END IF;

    RETURN NEW;
END;
$$;

CREATE TRIGGER trg_insert_subject_partial_records_by_partial_updated
    AFTER update
    ON academy.partial
    FOR EACH ROW
EXECUTE FUNCTION academy.insert_subject_partial_records_by_partial_updated();



-- Insert Grades by new subject_partial --
CREATE OR REPLACE FUNCTION academy.insert_grades_by_new_subject_partial()
    RETURNS TRIGGER LANGUAGE plpgsql AS
$$
DECLARE
BEGIN
    WITH query_aux AS (SELECT studentid FROM academy.student WHERE enrollmentid = new.enrollmentid)

    INSERT INTO academy.grade(studentid, subjectpartialid, grade, conductgrade, label)
    SELECT qa.studentid, new.subjectpartialid, 0, 0, 'AI' FROM query_aux qa;

    RETURN NEW;
END ;
$$;

CREATE TRIGGER trg_insert_grades_by_new_subject_partial
    AFTER insert
    ON academy.subject_partial
    FOR EACH ROW
EXECUTE FUNCTION academy.insert_grades_by_new_subject_partial();



-- Update enrollment quantity by new academy student --
CREATE OR REPLACE FUNCTION academy.update_enrollment_by_enroll_student()
    RETURNS TRIGGER AS $$
DECLARE
BEGIN
    UPDATE academy.enrollment SET quantity = quantity + 1 WHERE enrollmentid = NEW.enrollmentid;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_enrollment_by_enroll_student AFTER insert ON academy.student
    FOR EACH ROW EXECUTE FUNCTION academy.update_enrollment_by_enroll_student();