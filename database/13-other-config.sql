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
       sch.label AS schoolyear,
       enr.label AS enrollment
FROM secretary.student s LEFT JOIN
     (
         SELECT DISTINCT ON (aca.studentid) aca.studentid, aca.schoolyear, aca.enrollmentid
         FROM academy.student aca
         ORDER BY aca.studentid, aca.schoolyear DESC
     ) aca ON aca.studentid = s.studentid
                         LEFT JOIN secretary.schoolyear sch ON aca.schoolyear = sch.schoolyearid
                         LEFT JOIN academy.enrollment enr ON aca.enrollmentid = enr.enrollmentid;