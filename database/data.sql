set datestyle to 'European';

insert into config.user(name, secondname, surname, secondsurname, password, email, userstate, createdat, updatedat)
values ('Usuario', 'por', 'Defecto', 'del sistema', 'default123', 'user.default@cbl-edu.com', true, now(),now()),
       ('Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kt12345', 'kenny.tinoco@cbl-edu.com', true, now(),now()),
       ('Ezequiel', 'De jesús', 'Urbina', 'Zeledón', 'eu12345', 'ezequiel.urbina@cbl-edu.com', true, now(),now());


-- ############################## ---
insert into accounting.discount(discountid, description, amount, tag)
values (1, 'Sin descuento', 0, 'Sin descuento'),
       (2, 'Descuento por hijos', 0.07, 'Descuento básico'),
       (3, 'Descuento por hijos del personal', 0.5, 'Descuento trabajadores');

insert into accounting.tarifftype(description)
values ('Mensualidad'),
       ('Matrícula'),
       ('Útiles'),
       ('Otros');


INSERT INTO Accounting.cashier(cashierid, userid)
SELECT 'caj-eurbina', u.userid FROM config.user u
WHERE u.name = 'Ezequiel';

INSERT INTO Accounting.cashier(cashierid, userid)
SELECT 'caj-ktinoco', u.userid FROM config.user u
WHERE u.name = 'Kenny';


INSERT INTO academy.teacher(teacherid, userid, isguide)
SELECT 'tch-001', u.userid, false FROM config.user u
WHERE u.name = 'Usuario';;