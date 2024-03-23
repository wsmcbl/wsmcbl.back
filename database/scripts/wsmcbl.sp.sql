create view obj.accounting.student as
    select (s.studentId, s.name, s.secondname, s.surname, s.secondname, e.grade + e.section, '2024', s.tutor) from accounting.student as s
inner join academy.enrollment as e on s.enrollmentid = e.enrollmentid 