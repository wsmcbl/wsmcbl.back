set datestyle to 'European';

insert into config.user(name, secondname, surname, secondsurname, email, userstate, createdat, updatedat, password)
values ('Usuario', 'por', 'Defecto', 'del sistema', 'user.default@cbl-edu.com', true, now(),now(), 'AQAAAAIAAYagAAAAEBA+otefABAFYU//4mkRSCB+4Ehre7sDid871rFP7vW3snwji5+cxvjXsWUa1AasZw=='),
       ('Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kenny.tinoco@cbl-edu.com', true, now(),now(), 'AQAAAAIAAYagAAAAEOKy+ElmYTF+ClhQ68aqO1TCREwarjQMzylachhHEo0/duwGTqkAf5IWQRQeNdEH+g=='),
       ('Ezequiel', 'De jesús', 'Urbina', 'Zeledón', 'ezequiel.urbina@cbl-edu.com', true, now(),now(),'AQAAAAIAAYagAAAAEK/ObbY+PMQMXK/Q2rqJQyZKPUwiZGPALh/Bww0t6j9gozilS/PVoYQfLo8eDoHmFA==');


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