set datestyle to 'European';
    
insert into config.user(userid, name, secondname, surname, secondsurname, username, password, email, userstate)
values ('1002', 'Kenny', 'Jordan', 'Tinoco', 'Cerda', 'kt-user1002', '54321', 'kennytinoco@gmail.com', true),
       ('1003', 'Mateo', 'José', 'Mercado', 'Parrila', 'mm-user1003', 'mjmp12345', 'ficticio@gmail.com', false),
       ('1001', 'Ezequilito', 'De jesús', 'Urbina', 'Zeledón', 'ez-user1001', '12345', 'ezeurxoxoxo@gmail.com', true);


-- ############################## ---
insert into accounting.discount(discountid, description, amount, tag)
values (1, 'Sin descuento', 0, 'Sin descuento'),
       (2, 'Descuento por hijos', 0.07, 'Descuento básico'),
       (3, 'Descuento por hijos del personal', 0.5, 'descuento trabajadores');

insert into accounting.tarifftype(description)
values ('Mensualidad'),
       ('Matricula'),
       ('Utiles'),
       ('Otros');

insert into accounting.cashier(cashierid, userid)
values ('caj-eurbina', '1001'),
       ('caj-ktinoco', '1002'),
       ('caj-mmercado', '1003');
