set datestyle to 'European';

-- delete from secretary.student where true;

insert into config.user(userid, name, secondname, surname, secondsurname, username, password, email, userstate) values 
('1001', 'Ezequilito', 'De jesús', 'Urbina', 'Zeledón', 'ez-user1001', '12345', 'ezequielurbinaxoxoxo@gmail.com', true),
('1002', 'Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kt-user1002', '54321', 'kennytinoco@gmail.com', true),
('1003', 'Mateo', 'José', 'Mercado', 'Parrila', 'mm-user1003', 'mjmp12345', 'ficticio@gmail.com', false);


-- ############################## ---
insert into secretary.schoolyear(schoolyearid, label, startdate, deadline, isactive) values
('2024', 'Año lectivo 2024', '01/01/2024', '31/12/2024', true),
('2023', 'Año lectivo 2023', '01/01/2023', '31/12/2023', false),
('2022', 'Año lectivo 2022', '01/01/2022', '31/12/2022', false),
('2021', 'Año lectivo 2021', '01/01/2021', '31/12/2021', false),
('2019', 'Año lectivo 2019', '01/01/2019', '31/12/2019', false);

insert into secretary.grade(gradelabel, schoolyear, quantity, modality) values
('2019.11vo', '2019', 100, 'secundaria'),
('2023.1ro', '2023', 100, 'primaria'),
('2023.2do', '2023', 100, 'primaria'),
('2023.3ro', '2023', 100, 'primaria'),
('2023.4to', '2023', 100, 'primaria'),
('2023.5to', '2023', 100, 'primaria'),
('2023.6to', '2023', 100, 'primaria'),
('2023.7mo', '2023', 100, 'secundaria'),
('2023.8vo', '2023', 100, 'secundaria'),
('2023.9no', '2023', 100, 'secundaria'),
('2023.10mo', '2023', 100, 'secundaria'),
('2023.11vo', '2023', 100, 'secundaria'),
('2024.1ro', '2024', 100, 'primaria'),
('2024.2do', '2024', 100, 'primaria'),
('2024.3ro', '2024', 100, 'primaria'),
('2024.4to', '2024', 100, 'primaria'),
('2024.5to', '2024', 100, 'primaria'),
('2024.6to', '2024', 100, 'primaria'),
('2024.7mo', '2024', 100, 'secundaria'),
('2024.8vo', '2024', 100, 'secundaria'),
('2024.9no', '2024', 100, 'secundaria'),
('2024.10mo', '2024', 100, 'secundaria'),
('2024.11vo', '2024', 100, 'secundaria');

-- puede fallar por el gradeId autogenerado
insert into secretary.subject(subjectid, gradeid, name) values
('sjt001', 23, 'Matematicas'),
('sjt002', 23, 'Español'),
('sjt003', 22, 'Quimica'),
('sjt004', 23, 'Español');

insert into secretary.student(name, secondname, surname, secondsurname, studentstate, schoolyear, tutor, sex, birthday) values
('Kenny', 'Jordan', 'Tinoco', 'Cerda', true, 2024, 'Felix Tinoco', false, '01/05/2001'),
('Leonarno', 'Alberto', 'Muñoz', 'Morales', false, 2022, 'Isabel Morales', false, '05/08/2002'),
('Emilio', 'Fabian', 'Brenes', 'Rodriguez', true, 2023, 'Noe Brenes', false, '15/03/2002'),
('Noelia', 'Abigail', 'Guzmán', 'Martínez', true, 2024, 'Josué Guzman', true, '11/10/2002'),
('Jeniffer', 'Alexandra', 'Rojas', 'Ríos', true, 2019, 'Ezequielito Urbina', true, '01/01/2000'),
('Escarleth','Guadalupe', 'Chávez', 'Cajina', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Edrei','Jerameel', 'Hernández', 'Zapata', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Miguel','Angel', 'Perez', 'Amador', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Zoe','Denisse', 'Manzano', 'Bonilla', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Joseph','Guillermo', 'Valdivia', 'Montano', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Hamiltthon','José', 'Osorio', 'Castillo', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Jeanelly','Anasereth', 'Tercero', 'Martinez', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Rossmery','Leonor', 'Moreno', 'Reyes', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Josué','David', 'Pérez', 'Urbina', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Brittany','Fabiola', 'Reyes', 'Pantoja', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Luciana','Isabel', 'Romero', 'Zepeda', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Jordan','Jesús', 'Urtecho', 'Caballero', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Jorgleny','Dayanna', 'Guerrero', 'García', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Snayder','Noe', 'Leiva', 'Rivera', true, 2024, 'Persona Ficticia', true, '10/05/2009'),
('Yasmin','Guisell', 'Salazar', 'Silva', true, 2024, 'Persona Ficticia', true, '10/05/2009');                                 


-- ############################## ---
insert into academy.enrollment (enrollmentid, enrollmentlabel, section, schoolyear, capacity, quantity, gradeid) values
('2019.11voA', '11A', 'A', '2019', 30, 28, 1),
('2023.9noA', '9A', 'A', '2023', 25, 25, 10),
('2023.9noB', '9B', 'B', '2023', 24, 24, 10),
('2023.9noC', '9C', 'C', '2023', 20, 19, 10),
('2024.1roA', '1A', 'A', '2024', 28, 28, 13),
('2024.1roB', '1B', 'B', '2024', 28, 28, 13),
('2024.1roC', '1C', 'C', '2024', 28, 20, 13),
('2024.2doA', '2A', 'A', '2024', 30, 30, 13),
('2024.3roA', '3A', 'A', '2024', 32, 32, 13),
('2024.4toA', '4A', 'A', '2024', 28, 28, 13),
('2024.5toA', '5A', 'A', '2024', 28, 25, 13),
('2024.6toA', '6A', 'A', '2024', 30, 30, 13),
('2024.6toB', '6B', 'B', '2024', 25, 19, 13),
('2024.7moA', '7A', 'A', '2024', 28, 27, 13),
('2024.8voA', '8A', 'A', '2024', 30, 30, 13),
('2024.9noA', '9A', 'A', '2024', 30, 30, 13),
('2024.10moA', '10A', 'A', '2024', 28, 28, 13),
('2024.11voA', '11A', 'A', '2024', 28, 28, 13),
('2024.11voB', '11B', 'B', '2024', 25, 20, 13);

-- puede fallar el studentId autogenerado
insert into academy.student(studentid, enrollmentid, schoolyear, isapproved) values
('2024-0001-kjtc', '2024.11voA', '2024', false),
('2024-0005-jarr', '2024.11voA', '2024', false);

insert into academy.teacher(teacherid, userid, enrollmentid) values 
('tch-001', '1001', '2024.9noA'),
('tch-002', '1002', '2024.5toA'),
('tch-003', '1001', '2024.10moA');                                                                 

insert into academy.subject(subjectid, basesubjectid, teacherid, enrollmentid) values 
('sjt001-11', 'sjt001', 'tch-001', '2024.11voA'),
('sjt002-11', 'sjt002', 'tch-002', '2024.11voA'),
('sjt003-11', 'sjt003', 'tch-003', '2024.11voA'),
('sjt004-10', 'sjt004', 'tch-002', '2024.10moA');


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

insert into accounting.tariff(schoolyear, concept, amount, duedate, late, typeid)  values
('2023', 'Pago mes noviembre', 700, '28/11/2023', true, 1),
('2023', 'Pago treceavo mes', 700, '28/12/2023', true, 1),
('2024', 'Pago de matricula', 1800, null, false, 3),
('2024', 'Pago mes febrero', 800, '28/02/2024', false, 1),
('2024', 'Pago mes marzo', 800, '28/03/2024', false, 1),
('2024', 'Pago mes abril', 800, '28/04/2024', false, 1),
('2024', 'Pago mes mayo', 800, '28/05/2024', false, 1),
('2024', 'Pago mes junio', 800, '28/06/2024', false, 1),
('2024', 'Pago mes julio', 800, '28/07/2024', false, 1),
('2024', 'Pago mes agosto', 800, '28/08/2024', false, 1),
('2024', 'Pago mes septiembre', 800, '28/09/2024', false, 1),
('2024', 'Pago mes octubre', 800, '28/10/2024', false, 1),
('2024', 'Pago mes noviembre', 800, '28/11/2024', false, 1),
('2023', 'Pago treceavo mes', 700, '28/12/2024', false, 1),
('2024', 'Pago buzo escolar', 500, null, false, 2),
('2024', 'Pago uniforme escolar', 300, null, false, 2),
('2024', 'Pago excursión', 800, null, false, 3);