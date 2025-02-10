set datestyle to 'European';

insert into config.role(name, description)
values ('admin', 'Full system access.'),
       ('secretary', 'Access to the secretary and academy modules.'),
       ('cashier', 'Access to the accounting module.'),
       ('teacher', 'Access to the academic module.'),
       ('principal', 'Access to the management module.'),
       ('viceprincipal', 'Access to the management module.');

insert into config.user(roleid, name, secondname, surname, secondsurname, email, userstate, createdat, updatedat, password)
values (4, 'Usuario', 'por', 'Defecto', 'del sistema', 'user.default@cbl-edu.com', true, now(), now(),'AQAAAAIAAYagAAAAEBA+otefABAFYU//4mkRSCB+4Ehre7sDid871rFP7vW3snwji5+cxvjXsWUa1AasZw=='),
       (1, 'Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kenny.tinoco@cbl-edu.com', true, now(), now(),'AQAAAAIAAYagAAAAEOKy+ElmYTF+ClhQ68aqO1TCREwarjQMzylachhHEo0/duwGTqkAf5IWQRQeNdEH+g=='),
       (1, 'Ezequiel', 'De jesús', 'Urbina', 'Zeledón', 'ezequiel.urbina@cbl-edu.com', true, now(), now(),'AQAAAAIAAYagAAAAEK/ObbY+PMQMXK/Q2rqJQyZKPUwiZGPALh/Bww0t6j9gozilS/PVoYQfLo8eDoHmFA=='),
       (1, 'Thelma', null, 'Ríos', 'Zeas', 'thelma.rios@cbl-edu.com', true, now(), now(),'AQAAAAIAAYagAAAAEMexOu51jrRZOshRdeF0yZ5wm3HCcmvzJLuZI1aIeX1h4Mcfwx6BbHJMi+UyaZlUDA==');




insert into config.permission(name, spanishname, area, description)
values ('student:create', 'Crear estudiantes', 'secretary','Permission for the creation of students in the secretary scheme.'),
       ('student:update', 'Modificar estudiantes', 'secretary','Permission for the update of students in the secretary scheme.'),
       ('student:read', 'Ver estudiantes', 'secretary','Permission for the reading of students in the secretary scheme.'),
       ('user:update', 'Modificar perfiles de usuarios', 'config', 'Permission for update user profiles.'),
       ('user:read', 'Ver perfiles de usuarios', 'config', 'Permission for reading user profiles.'),
       ('rol:read', 'Ver roles', 'config', 'Permission for reading roles.'),
       ('permission:read', 'Ver permisos', 'config', 'Permission for reading permissions.'),
       ('partial:update', 'Modificar parciales', 'academy', 'Permission for update partials.'),
       ('partial:read', 'Ver parciales', 'academy', 'Permission for reading partials.'),
       ('report:read', 'Ver reportes', 'academy', 'Permission for reading report.'),
       ('transaction:create', 'Crear transacciones', 'accounting', 'Permission for creation transactions.'),
       ('transaction:update', 'Modificar transacciones', 'accounting', 'Permission for update transactions.'),
       ('transaction:read', 'Ver transacciones', 'accounting', 'Permission for reading transactions.'),
       ('tariff:update', 'Modificar tarifas', 'accounting', 'Permission for update transactions.'),
       ('tariff:read', 'Ver tarifas', 'accounting', 'Permission for reading transactions.');

-- Admin --
insert into config.role_permission(roleid, permissionid)
values (1, 1),
       (1, 2),
       (1, 3),
       (1, 4),
       (1, 5),
       (1, 6),-- TEMPORAL
       (1, 7),-- TEMPORAL
       (1, 8),-- TEMPORAL
       (1, 9),-- TEMPORAL
       (1, 10),-- TEMPORAL
       (1, 11),-- TEMPORAL
       (1, 12),-- TEMPORAL
       (1, 13),-- TEMPORAL
       (1, 14);-- TEMPORAL

-- Cashier --
insert into config.role_permission(roleid, permissionid)
values (3, 1),
       (3, 2),
       (3, 4),
       (1, 8),
       (1, 9),
       (1, 10);

insert into config.role_permission(roleid, permissionid)
values (2, 1),
       (2, 2),
       (2, 3),
       (2, 6),
       (4, 2),
       (4, 3),
       (5, 3),
       (5, 4),
       (5, 6);


-- ############################## ---
insert into accounting.discount(discountid, description, tag)
values (1, 'Sin descuento', 'Sin descuento'),
       (2, 'Descuento por hijos', 'Descuento básico'),
       (3, 'Descuento por hijos del personal', 'Descuento trabajadores');

insert into accounting.discounteducationallevel(del, discountId, educationalLevel, amount)
values (1, 1, 1, 0),
       (2, 1, 2, 0),
       (3, 1, 3, 0),
       (4, 2, 1, 0.08),
       (5, 2, 2, 0.08),
       (6, 2, 3, 0.08),
       (7, 3, 1, 0.5),
       (8, 3, 2, 0.5),
       (9, 3, 3, 0.5);

insert into accounting.tarifftype(description)
values ('Mensualidad'),
       ('Matrícula'),
       ('Útiles'),
       ('Otros');


INSERT INTO Accounting.cashier(cashierid, userid)
SELECT 'caj-eurbina', u.userid
FROM config.user u
WHERE u.name = 'Ezequiel';

INSERT INTO Accounting.cashier(cashierid, userid)
SELECT 'caj-ktinoco', u.userid
FROM config.user u
WHERE u.name = 'Kenny';

INSERT INTO academy.teacher(userid, isguide)
SELECT u.userid, false
FROM config.user u
WHERE u.name = 'Usuario';