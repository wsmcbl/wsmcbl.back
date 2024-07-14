set datestyle to 'European';

-- delete from secretary.student where true;

insert into config.user(userid, name, secondname, surname, secondsurname, username, password, email, userstate) values 
('1001', 'Ezequilito', 'De jesús', 'Urbina', 'Zeledón', 'ez-user1001', '12345', 'ezequielurbinaxoxoxo@gmail.com', true),
('1002', 'Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kt-user1002', '54321', 'kennytinoco@gmail.com', true),
('1003', 'Mateo', 'José', 'Mercado', 'Parrila', 'mm-user1003', 'mjmp12345', 'ficticio@gmail.com', false);
    
insert into accounting.discount(discountid, description, amount, tag) values
(1, 'Sin descuento', 0, 'Sin descuento'),
(2, 'Descuento por hijos', 0.07, 'descuento básico'),
(3, 'Descuento por hijos del personal', 0.5, 'descuento trabajadores');

insert into accounting.tarifftype(description) values
('Mensualidad'),
('Utiles'),
('Otros');

insert into secretary.schoolyear(schoolyearid, label, startdate, deadline, isactive) values
('2024', 'Año lectivo 2024', '01/01/2024', '31/12/2024', true),
('2023', 'Año lectivo 2023', '01/01/2023', '31/12/2023', false),
('2022', 'Año lectivo 2022', '01/01/2022', '31/12/2022', false),
('2021', 'Año lectivo 2021', '01/01/2021', '31/12/2021', false),
('2019', 'Año lectivo 2019', '01/01/2019', '31/12/2019', false);


insert into secretary.grade(gradelabel, schoolyear) values
('2019.11vo', '2019'),
('2023.1ro', '2023'),
('2023.2do', '2023'),
('2023.3ro', '2023'),
('2023.4to', '2023'),
('2023.5to', '2023'),
('2023.6to', '2023'),
('2023.7mo', '2023'),
('2023.8vo', '2023'),
('2023.9no', '2023'),
('2023.10mo', '2023'),
('2023.11vo', '2023'),
('2024.1ro', '2024'),
('2024.2do', '2024'),
('2024.3ro', '2024'),
('2024.4to', '2024'),
('2024.5to', '2024'),
('2024.6to', '2024'),
('2024.7mo', '2024'),
('2024.8vo', '2024'),
('2024.9no', '2024'),
('2024.10mo', '2024'),
('2024.11vo', '2024'); 
                                            

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

insert into accounting.cashier(cashierid, userid)  values
('caj-eurbina', '1001'),
('caj-ktinoco', '1002'),
('caj-mmercado', '1003');                                    

insert into secretary.student (name, secondname, surname, secondsurname, studentstate, schoolyear, tutor, sex, birthday, enrollmentlabel) values
('Kenny', 'Jordan', 'Tinoco', 'Cerda', true, 2024, 'Felix Tinoco', false, '01/05/2001', '11voA'),
('Leonarno', 'Alberto', 'Muñoz', 'Morales', false, 2022, 'Isabel Morales', false, '05/08/2002', '11voA'),
('Emilio', 'Fabian', 'Brenes', 'Rodriguez', true, 2023, 'Noe Brenes', false, '15/03/2002', '10moA'),
('Noelia', 'Abigail', 'Guzmán', 'Martínez', true, 2024, 'Josué Guzman', true, '11/10/2002', '11voA'),
('Jeniffer', 'Alexandra', 'Rojas', 'Ríos', true, 2019, 'Ezequielito Urbina', true, '01/01/2000', '11voA'),
('Escarleth','Guadalupe', 'Chávez', 'Cajina', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Edrei','Jerameel', 'Hernández', 'Zapata', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Miguel','Angel', 'Perez', 'Amador', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Zoe','Denisse', 'Manzano', 'Bonilla', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Joseph','Guillermo', 'Valdivia', 'Montano', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Hamiltthon','José', 'Osorio', 'Castillo', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Jeanelly','Anasereth', 'Tercero', 'Martinez', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Rossmery','Leonor', 'Moreno', 'Reyes', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Josué','David', 'Pérez', 'Urbina', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Brittany','Fabiola', 'Reyes', 'Pantoja', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Luciana','Isabel', 'Romero', 'Zepeda', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Jordan','Jesús', 'Urtecho', 'Caballero', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Jorgleny','Dayanna', 'Guerrero', 'García', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Snayder','Noe', 'Leiva', 'Rivera', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA'),
('Yasmin','Guisell', 'Salazar', 'Silva', true, 2024, 'Persona Ficticia', true, '10/05/2009', '9noA');


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