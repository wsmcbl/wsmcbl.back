CREATE OR REPLACE PROCEDURE accounting.getLastTransactionByStudent(in id varchar(20), out lastTransaction RECORD)
    LANGUAGE plpgsql
AS $$
BEGIN
    SELECT * INTO  lastTransaction FROM accounting.transaction as t
    WHERE t.studentid = id
      AND t.date >= DATE_TRUNC('day', CURRENT_DATE)
      AND t.date < DATE_TRUNC('day', CURRENT_DATE + INTERVAL '1 day')
    ORDER BY t.date DESC
    LIMIT 1;    
END;
$$;


CALL accounting.getLastTransactionByStudent('2024-2039SABS', @las);
