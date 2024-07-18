set datestyle to 'European';

-- delete from secretary.student where true;

insert into config.user(userid, name, secondname, surname, secondsurname, username, password, email, userstate) values 
('1001', 'Ezequilito', 'De jesús', 'Urbina', 'Zeledón', 'ez-user1001', '12345', 'ezequielurbinaxoxoxo@gmail.com', true),
('1002', 'Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kt-user1002', '54321', 'kennytinoco@gmail.com', true),
('1003', 'Mateo', 'José', 'Mercado', 'Parrila', 'mm-user1003', 'mjmp12345', 'ficticio@gmail.com', false);


-- ############################## ---
insert into secretary.schoolyear(label, startdate, deadline, isactive) values
('2024', '01/01/2024', '31/12/2024', true),
('2023', '01/01/2023', '31/12/2023', false),
('2022', '01/01/2022', '31/12/2022', false),
('2021', '01/01/2021', '31/12/2021', false),
('2019', '01/01/2019', '31/12/2019', false);

insert into secretary.grade(gradelabel, schoolyear, quantity, modality) values
('11vo', 'sch011', 100, 'secundaria'),
('1ro', 'sch011', 100, 'primaria'),
('2do', 'sch011', 100, 'primaria'),
('3ro', 'sch011', 100, 'primaria'),
('4to', 'sch011', 100, 'primaria'),
('5to', 'sch011', 100, 'primaria'),
('6to', 'sch011', 100, 'primaria'),
('7mo', 'sch011', 100, 'secundaria'),
('8vo', 'sch011', 100, 'secundaria'),
('9no', 'sch011', 100, 'secundaria'),
('10mo', 'sch011', 100, 'secundaria'),
('11vo', 'sch011', 100, 'secundaria'),
('1ro', 'sch010', 100, 'primaria'),
('2do', 'sch010', 100, 'primaria'),
('3ro', 'sch010', 100, 'primaria'),
('4to', 'sch010', 100, 'primaria'),
('5to', 'sch010', 100, 'primaria'),
('6to', 'sch010', 100, 'primaria'),
('7mo', 'sch010', 100, 'secundaria'),
('8vo', 'sch010', 100, 'secundaria'),
('9no', 'sch010', 100, 'secundaria'),
('10mo', 'sch010', 100, 'secundaria'),
('11vo', 'sch010', 100, 'secundaria');

-- puede fallar por el gradeId autogenerado
insert into secretary.subject(gradeid, name, ismandatory) values
('gd00034', 'Matematicas', true),
('gd00033', 'Español', true),
('gd00034', 'Quimica', true),
('gd00034', 'Español', true);

insert into secretary.student(name, secondname, surname, secondsurname, studentstate, schoolyear, tutor, sex, birthday) values
('Kenny', 'Jordan', 'Tinoco', 'Cerda', true, 'sch010', 'Felix Tinoco', false, '01/05/2001'),
('Leonarno', 'Alberto', 'Muñoz', 'Morales', false, 'sch012', 'Isabel Morales', false, '05/08/2002'),
('Emilio', 'Fabian', 'Brenes', 'Rodriguez', true, 'sch011', 'Noe Brenes', false, '15/03/2002'),
('Noelia', 'Abigail', 'Guzmán', 'Martínez', true, 'sch010', 'Josué Guzman', true, '11/10/2002'),
('Jeniffer', 'Alexandra', 'Rojas', 'Ríos', true, 'sch015', 'Ezequielito Urbina', true, '01/01/2000'),
('Escarleth','Guadalupe', 'Chávez', 'Cajina', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Edrei','Jerameel', 'Hernández', 'Zapata', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Miguel','Angel', 'Perez', 'Amador', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Zoe','Denisse', 'Manzano', 'Bonilla', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Joseph','Guillermo', 'Valdivia', 'Montano', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Hamiltthon','José', 'Osorio', 'Castillo', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Jeanelly','Anasereth', 'Tercero', 'Martinez', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Rossmery','Leonor', 'Moreno', 'Reyes', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Josué','David', 'Pérez', 'Urbina', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Brittany','Fabiola', 'Reyes', 'Pantoja', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Luciana','Isabel', 'Romero', 'Zepeda', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Jordan','Jesús', 'Urtecho', 'Caballero', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Jorgleny','Dayanna', 'Guerrero', 'García', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Snayder','Noe', 'Leiva', 'Rivera', true, 'sch010', 'Persona Ficticia', true, '10/05/2009'),
('Yasmin','Guisell', 'Salazar', 'Silva', true, 'sch010', 'Persona Ficticia', true, '10/05/2009');                                 


-- ############################## ---
insert into academy.enrollment (enrollmentlabel, section, schoolyear, capacity, quantity, gradeid) values
('11A', 'A', 'sch014', 30, 28, 'gd00034'),
('9A', 'A', 'sch011', 25, 25, 'gd00044'),
('9B', 'B', 'sch011', 24, 24, 'gd00044'),
('9C', 'C', 'sch011', 20, 19, 'gd00044'),
('1A', 'A', 'sch010', 28, 28, 'gd00037'),
('1B', 'B', 'sch010', 28, 28, 'gd00037'),
('1C', 'C', 'sch010', 28, 20, 'gd00037'),
('2A', 'A', 'sch010', 30, 30, 'gd00037'),
('3A', 'A', 'sch010', 32, 32, 'gd00037'),
('4A', 'A', 'sch010', 28, 28, 'gd00037'),
('5A', 'A', 'sch010', 28, 25, 'gd00037'),
('6A', 'A', 'sch010', 30, 30, 'gd00037'),
('6B', 'B', 'sch010', 25, 19, 'gd00037'),
('7A', 'A', 'sch010', 28, 27, 'gd00037'),
('8A', 'A', 'sch010', 30, 30, 'gd00037'),
('9A', 'A', 'sch010', 30, 30, 'gd00037'),
('10A', 'A', 'sch010', 28, 28, 'gd00037'),
('11A', 'A', 'sch010', 28, 28, 'gd00037'),
('11B', 'B', 'sch010', 25, 20, 'gd00037');

-- puede fallar el studentId autogenerado
insert into academy.student(studentid, enrollmentid, schoolyear, isapproved) values
('2024-0081-kjtc', 'enr00086', 'sch010', false),
('2024-0085-jarr', 'enr00086', 'sch010', false);

insert into academy.teacher(teacherid, userid, enrollmentid) values 
('tch-001', '1001', 'enr00086'),
('tch-002', '1002', 'enr00086'),
('tch-003', '1001', 'enr00088');                                                                 

insert into academy.subject(subjectid, teacherid, enrollmentid) values 
('sub000014', 'tch-001', 'enr00086'),
('sub000015', 'tch-002', 'enr00086'),
('sub000017', 'tch-003', 'enr00086'),
('sub000016', 'tch-002', 'enr00087');


-- ############################## ---
insert into accounting.discount(discountid, description, amount, tag) values
(1, 'Sin descuento', 0, 'Sin descuento'),
(2, 'Descuento por hijos', 0.07, 'descuento básico'),
(3, 'Descuento por hijos del personal', 0.5, 'descuento trabajadores');

insert into accounting.tarifftype(description) values
('Mensualidad'),
('Utiles'),
('Otros');

insert into accounting.cashier(cashierid, userid)  values
('caj-eurbina', '1001'),
('caj-ktinoco', '1002'),
('caj-mmercado', '1003');                                    

insert into accounting.tariff(schoolyear, concept, amount, duedate, late, typeid, modality)  values
('sch011', 'Pago mes noviembre', 700, '28/11/2023', true, 1, 3),
('sch011', 'Pago treceavo mes', 700, '28/12/2023', true, 1, 3),
('sch010', 'Pago de matricula', 1800, null, false, 3, 3),
('sch010', 'Pago mes febrero', 800, '28/02/2024', false, 1, 3),
('sch010', 'Pago mes marzo', 800, '28/03/2024', false, 1, 3),
('sch010', 'Pago mes abril', 800, '28/04/2024', false, 1, 3),
('sch010', 'Pago mes mayo', 800, '28/05/2024', false, 1, 3),
('sch010', 'Pago mes junio', 800, '28/06/2024', false, 1, 3),
('sch010', 'Pago mes julio', 800, '28/07/2024', false, 1, 3),
('sch010', 'Pago mes agosto', 800, '28/08/2024', false, 1, 3),
('sch010', 'Pago mes septiembre', 800, '28/09/2024', false, 1, 3),
('sch010', 'Pago mes octubre', 800, '28/10/2024', false, 1, 3),
('sch010', 'Pago mes noviembre', 800, '28/11/2024', false, 1, 3),
('sch011', 'Pago treceavo mes', 700, '28/12/2024', false, 1, 3),
('sch010', 'Pago buzo escolar', 500, null, false, 2, 3),
('sch010', 'Pago uniforme escolar', 300, null, false, 2, 3),
('sch010', 'Pago excursión', 800, null, false, 3, 3);