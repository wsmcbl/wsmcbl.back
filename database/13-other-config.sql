create table if not exists Config.Multimedia
(
    multimediaId serial not null primary key,
    schoolyear varchar(15) not null,
    type serial not null,
    value varchar(1500) not null,
    foreign key (schoolyear) references secretary.schoolyear
);


-- Transaction_Report_View view ---
CREATE VIEW accounting.transaction_report_view AS
SELECT t.transactionId,
       t.number,
       s.studentId,
       concat_ws(' ', s.name, s.secondname, s.surname, s.secondsurname) as studentName,
       t.total,
       t.date as dateTime,
       t.isvalid,
       e.label as enrollmentLabel,
       CASE
           WHEN COUNT(DISTINCT ta.typeId) = 1 THEN MAX(ta.typeId)
           ELSE 0
           END
                                                                        AS type
FROM accounting.transaction t
         JOIN
     secretary.student s ON t.studentId = s.studentId
         LEFT JOIN
     academy.student asd ON s.studentId = asd.studentId
         LEFT JOIN
     academy.enrollment e ON asd.enrollmentId = e.enrollmentId
         LEFT JOIN
     accounting.transaction_tariff tt ON t.transactionId = tt.transactionId
         LEFT JOIN
     accounting.tariff ta ON tt.tariffId = ta.tariffId
GROUP BY t.transactionId, t.number, s.studentId, s.name, s.secondname, s.surname, s.secondsurname, t.total, t.date,
         e.label, t.isvalid;


--- Student_View View ---
CREATE VIEW secretary.student_to_list_view AS
SELECT s.studentid,
       concat_ws(' ', s.name, s.secondname, s.surname, s.secondsurname) as fullName,
       s.studentstate,
       t.name as tutor,
       sch.label AS schoolyear,
       enr.label AS enrollment
FROM secretary.student s LEFT JOIN secretary.studenttutor t ON s.tutorid = t.tutorid LEFT JOIN
     (
         SELECT DISTINCT ON (aca.studentid) aca.studentid, aca.schoolyear, aca.enrollmentid
         FROM academy.student aca
         ORDER BY aca.studentid, aca.schoolyear DESC
     ) aca ON aca.studentid = s.studentid
LEFT JOIN secretary.schoolyear sch ON aca.schoolyear = sch.schoolyearid
LEFT JOIN academy.enrollment enr ON aca.enrollmentid = enr.enrollmentid;


--- debtor_student_View View ---
CREATE VIEW accounting.debtor_student_view AS
SELECT s.studentid,
       CONCAT_WS(' ', s.name, s.secondname, s.surname, s.secondsurname) AS fullName,
       sch.schoolyearid,
       sch.label AS schoolyear,
       enr.enrollmentid,
       enr.label AS enrollment,
       COUNT(deb.tariffid) AS quantity,
       SUM(deb.amount - deb.debtbalance) AS total
FROM secretary.student s
         LEFT JOIN (SELECT DISTINCT ON (aca.studentid) aca.studentid, aca.schoolyear, aca.enrollmentid
                    FROM academy.student aca ORDER BY aca.studentid, aca.schoolyear DESC) aca ON aca.studentid = s.studentid
         LEFT JOIN secretary.schoolyear sch ON aca.schoolyear = sch.schoolyearid
         LEFT JOIN academy.enrollment enr ON aca.enrollmentid = enr.enrollmentid
         JOIN accounting.debthistory deb ON deb.studentid = s.studentid
         JOIN accounting.tariff t on t.tariffid = deb.tariffid
WHERE s.studentstate = TRUE AND t.duedate < CURRENT_DATE AND deb.ispaid = FALSE
GROUP BY (s.studentid, sch.schoolyearid, sch.label, enr.enrollmentid, enr.label);