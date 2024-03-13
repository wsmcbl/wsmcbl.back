create database wsmcbl.database;
       
       create table Student
       (
           id varchar(100),
           name varchar(100),
           secondName varchar(100),
           surname varchar(100),
           secondSurname varchar(100),
           sex bool,
           birthday date
       );



INSERT INTO usuarios VALUES  
('2024-0478KJTC', 'Kenny', 'Jordan', 'Tinoco', 'Cerda', false, '2000-05-07 21:50:02');

INSERT INTO usuarios VALUES
('2024-1104LAMM', 'Leonarno', 'Alberto', 'Muñoz', 'Morales', false, NOW());

INSERT INTO usuarios VALUES
    ('2025-4571EFBR', 'Emilio', 'Fabian', 'Brenes', 'Rodriguez', false, NOW());

INSERT INTO usuarios VALUES
    ('2021-3041NAGM', 'Noelia', 'Abigail', 'Guzmán', 'Martínez', true, NOW());
