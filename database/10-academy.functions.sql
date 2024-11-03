-- Insert subject_partial by property gradeRecordIsActive true of partial updated --
CREATE OR REPLACE FUNCTION academy.insert_subject_partial_records_by_partial_updated()
    RETURNS TRIGGER AS
$$
DECLARE
    current_school_year VARCHAR;
BEGIN
    IF (new.isactive = TRUE and new.graderecordisactive = TRUE) THEN

        SELECT schoolyearid
        INTO current_school_year
        FROM secretary.schoolyear
        WHERE label = to_char(current_date, 'YYYY');

        WITH query_aux AS (SELECT s.*
                           FROM academy.subject s
                                    inner JOIN academy.enrollment e on s.enrollmentid = e.enrollmentid
                           WHERE e.schoolyear = current_school_year)

        INSERT
        INTO academy.subject_partial(subjectid, enrollmentid, partialid, teacherid)
        SELECT qa.subjectid, qa.enrollmentid, new.partialid, qa.teacherid
        FROM query_aux qa;

    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_subject_partial_records_by_partial_updated
    AFTER update
    ON academy.partial
    FOR EACH ROW
EXECUTE FUNCTION academy.insert_subject_partial_records_by_partial_updated();


-- Insert Grades by new subject_partial --
CREATE OR REPLACE FUNCTION academy.insert_grades_by_new_subject_partial()
    RETURNS TRIGGER AS
$$
DECLARE
BEGIN
    WITH query_aux AS (SELECT studentid FROM academy.student WHERE enrollmentid = new.enrollmentid)

    INSERT
    INTO academy.grade(studentid, subjectpartialid, grade, conductgrade, label)
    SELECT qa.studentid, new.subjectpartialid, 0, 0, 'AI' FROM query_aux qa;

    RETURN NEW;
END ;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_insert_grades_by_new_subject_partial
    AFTER insert
    ON academy.subject_partial
    FOR EACH ROW
EXECUTE FUNCTION academy.insert_grades_by_new_subject_partial();