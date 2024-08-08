create schema if not exists Academy;

-- Generate secretary.subject id
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