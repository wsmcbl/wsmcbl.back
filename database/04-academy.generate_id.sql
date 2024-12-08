create schema if not exists Academy;

-- Generate secretary.enrollment id
CREATE SEQUENCE if not exists academy.enrollment_id_seq START 10;

CREATE OR REPLACE FUNCTION academy.generate_enrollment_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(5);
BEGIN
    seq_part := LPAD(NEXTVAL('academy.enrollment_id_seq')::TEXT, 5, '0');

    Return 'enr' || seq_part;
END;
$$ LANGUAGE plpgsql;



-- Generate secretary.teacher id
CREATE SEQUENCE if not exists academy.teacher_id_seq START 2;

CREATE OR REPLACE FUNCTION academy.generate_teacher_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(3);
BEGIN
    seq_part := LPAD(NEXTVAL('academy.teacher_id_seq')::TEXT, 3, '0');
        
    Return 'tch-' || seq_part;
END;
$$ LANGUAGE plpgsql;