create schema if not exists Secretary;

-- Generate secretary.grade id
CREATE SEQUENCE if not exists secretary.schoolyear_id_seq START 10;

CREATE OR REPLACE FUNCTION secretary.generate_schoolyear_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(3);
BEGIN
    seq_part := LPAD(NEXTVAL('secretary.schoolyear_id_seq')::TEXT, 3, '0');

    Return 'sch' || seq_part;
END;
$$ LANGUAGE plpgsql;


-- Generate secretary.grade id
CREATE SEQUENCE if not exists secretary.grade_id_seq START 10;

CREATE OR REPLACE FUNCTION secretary.generate_grade_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(5);
BEGIN
    seq_part := LPAD(NEXTVAL('secretary.grade_id_seq')::TEXT, 5, '0');

    Return 'gd' || seq_part;
END;
$$ LANGUAGE plpgsql;


-- Generate secretary.subject id
CREATE SEQUENCE if not exists secretary.subject_id_seq START 10;

CREATE OR REPLACE FUNCTION secretary.generate_subject_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(6);
BEGIN
    seq_part := LPAD(NEXTVAL('secretary.subject_id_seq')::TEXT, 6, '0');

    Return 'sub' || seq_part;
END;
$$ LANGUAGE plpgsql;