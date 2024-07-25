set datestyle to 'European';

insert into config.user(userid, name, secondname, surname, secondsurname, username, password, email, userstate)
values ('1002', 'Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kt-user1002', '54321', 'kennytinoco@gmail.com', true),
       ('1003', 'Mateo', 'José', 'Mercado', 'Parrila', 'mm-user1003', 'mjmp12345', 'ficticio@gmail.com', false),
       ('1001', 'Ezequilito', 'De jesús', 'Urbina', 'Zeledón', 'ez-user1001', '12345', 'ezeurxoxoxo@gmail.com', true);


-- ############################## ---
insert into accounting.discount(discountid, description, amount, tag)
values (1, 'Sin descuento', 0, 'Sin descuento'),
       (2, 'Descuento por hijos', 0.07, 'descuento básico'),
       (3, 'Descuento por hijos del personal', 0.5, 'descuento trabajadores');

insert into accounting.tarifftype(description)
values ('Mensualidad'),
       ('Utiles'),
       ('Otros'),
       ('Matricula');

insert into accounting.cashier(cashierid, userid)
values ('caj-eurbina', '1001'),
       ('caj-ktinoco', '1002'),
       ('caj-mmercado', '1003');


-- ############################## ---
insert into secretary.student(name, secondname, surname, secondsurname, studentstate, schoolyear, sex, birthday, diseases, religion)
values ('Kenny', 'Jordan', 'Tinoco', 'Cerda', true, 'sch010', false, '01/05/2001', '', ''),
       ('Leonarno', 'Alberto', 'Muñoz', 'Morales', false, 'sch012', false, '05/08/2002', '', ''),
       ('Emilio', 'Fabian', 'Brenes', 'Rodriguez', true, 'sch011',  false, '15/03/2002', '', ''),
       ('Noelia', 'Abigail', 'Guzmán', 'Martínez', true, 'sch010',  true, '11/10/2002', '', ''),
       ('Jeniffer', 'Alexandra', 'Rojas', 'Ríos', true, 'sch015',  true, '01/01/2000', '', ''),
       ('Escarleth', 'Guadalupe', 'Chávez', 'Cajina', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Edrei', 'Jerameel', 'Hernández', 'Zapata', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Miguel', 'Angel', 'Perez', 'Amador', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Zoe', 'Denisse', 'Manzano', 'Bonilla', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Joseph', 'Guillermo', 'Valdivia', 'Montano', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Hamiltthon', 'José', 'Osorio', 'Castillo', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Jeanelly', 'Anasereth', 'Tercero', 'Martinez', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Rossmery', 'Leonor', 'Moreno', 'Reyes', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Josué', 'David', 'Pérez', 'Urbina', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Brittany', 'Fabiola', 'Reyes', 'Pantoja', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Luciana', 'Isabel', 'Romero', 'Zepeda', true, 'sch010', true, '10/05/2009', '', ''),
       ('Jordan', 'Jesús', 'Urtecho', 'Caballero', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Jorgleny', 'Dayanna', 'Guerrero', 'García', true, 'sch010', true, '10/05/2009', '', ''),
       ('Snayder', 'Noe', 'Leiva', 'Rivera', true, 'sch010',  true, '10/05/2009', '', ''),
       ('Yasmin', 'Guisell', 'Salazar', 'Silva', true, 'sch010', true, '10/05/2009', '', '');


-- ############################## ---
insert into academy.teacher(teacherid, userid, isguide)
values ('tch-001', '1001', true),
       ('tch-002', '1002', true),
       ('tch-003', '1001', true);