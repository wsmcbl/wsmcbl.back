create schema if not exists Accounting;

-- Generate accounting.transaction id
CREATE SEQUENCE if not exists accounting.transaction_id_seq START 1000;
CREATE SEQUENCE if not exists accounting.transaction_number_seq START 1000;

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


-- Generate accounting.cashier id
CREATE SEQUENCE if not exists accounting.cashier_id_seq START 1;

CREATE OR REPLACE FUNCTION Accounting.generate_cashier_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(6);
BEGIN
    seq_part := LPAD(NEXTVAL('accounting.cashier_id_seq')::TEXT, 3, '0');

    RETURN 'caj-' || seq_part;
END;
$$ LANGUAGE plpgsql;