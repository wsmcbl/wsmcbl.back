set datestyle to 'European';

insert into secretary.subjectarea(name)
values ('Desarrollo de las habilidades de la comunicación y Talento Artístico y Cultural'),
       ('Desarrollo Personal, Social y Emocional'),
       ('Desarrollo del Pensamiento Lógico y Científico');


insert into secretary.tariffcatalog(concept, amount, duedate, typeid, educationallevel)
values ('Pago de matrícula', 0, null, 2, 3),
       ('Pago mes febrero', 0, '28/02/2024', 1, 3),
       ('Pago mes marzo', 0, '28/03/2024', 1, 3),
       ('Pago mes abril', 0, '28/04/2024', 1, 3),
       ('Pago mes mayo', 0, '28/05/2024', 1, 3),
       ('Pago mes junio', 0, '28/06/2024', 1, 3),
       ('Pago mes julio', 0, '28/07/2024', 1, 3),
       ('Pago mes agosto', 0, '28/08/2024', 1, 3),
       ('Pago mes septiembre', 0, '28/09/2024', 1, 3),
       ('Pago mes octubre', 0, '28/10/2024', 1, 3),
       ('Pago mes noviembre', 0, '28/11/2024', 1, 3),
       ('Pago mes diciembre', 0, '28/12/2024', 1, 3),
       ('Pago treceavo mes', 0, '28/12/2024', 1, 3);

insert into secretary.degreecatalog(label, educationalLevel, tag)
values ('Primer Nivel', 1, '01'),
       ('Segundo Nivel', 1, '02'),
       ('Tercer Nivel', 1, '03'),
       ('Primer Grado', 2, '04'),
       ('Segundo Grado', 2, '05'),
       ('Tercer Grado', 2, '06'),
       ('Cuarto Grado', 2, '07'),
       ('Quinto Grado', 2, '08'),
       ('Sexto Grado', 2, '09'),
       ('Septimo Grado', 3, '10'),
       ('Octavo Grado', 3, '11'),
       ('Noveno Grado', 3, '12'),
       ('Décimo Grado', 3, '13'),
       ('Undécimo Grado', 3, '14');


-- #####################################################################################

-- I nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (1, false, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (1, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (1, false, 3, 'Matemáticas', 'Mat', 3, 1);

-- II nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (2, false, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (2, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (2, false, 3, 'Matemáticas', 'Mat', 3, 1);

-- III nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (3, false, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (3, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (3, false, 3, 'Matemáticas', 'Mat', 3, 1);

-- #####################################################################################

-- 1er grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (4, false, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (4, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (4, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (4, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (4, false, 3, 'Educación Física', 'EF', 2, 2),
       (4, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (4, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (4, false, 3, 'Conociendo mi Mundo', 'CM', 2, 5),
       (4, false, 3, 'Matemáticas', 'Mat', 3, 5),
       (4, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (4, false, 3, 'Computación', 'COMP', 3, 7),
       (4, false, 3, 'Conducta', 'COND', 3, 8);


-- 2do grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (5, false, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (5, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (5, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (5, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (5, false, 3, 'Educación Física', 'EF', 2, 2),
       (5, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (5, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (5, false, 3, 'Conociendo mi Mundo', 'CM', 2, 5),
       (5, false, 3, 'Matemáticas', 'Mat', 3, 5),
       (5, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (5, false, 3, 'Computación', 'COMP', 3, 7),
       (5, false, 3, 'Conducta', 'COND', 3, 8);


-- 3er grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (6, true, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (6, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (6, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (6, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (6, false, 3, 'Educación Física', 'EF', 2, 2),
       (6, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (6, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (6, true, 3, 'Estudios Sociales', 'ES', 2, 5),
       (6, true, 3, 'Ciencias Naturales', 'CN', 3, 1),
       (6, true, 3, 'Matemáticas', 'Mat', 3, 5),
       (6, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (6, false, 3, 'Computación', 'COMP', 3, 7),
       (6, false, 3, 'Conducta', 'COND', 3, 8);


-- 4to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (7, true, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (7, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (7, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (7, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (7, false, 3, 'Educación Física', 'EF', 2, 2),
       (7, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (7, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (7, true, 3, 'Estudios Sociales', 'ES', 2, 5),
       (7, true, 3, 'Ciencias Naturales', 'CN', 3, 1),
       (7, true, 3, 'Matemáticas', 'Mat', 3, 5),
       (7, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (7, false, 3, 'Computación', 'COMP', 3, 7),
       (7, false, 3, 'Conducta', 'COND', 3, 8);


-- 5to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (8, true, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (8, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (8, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (8, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (8, false, 3, 'Educación Física', 'EF', 2, 2),
       (8, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (8, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (8, true, 3, 'Estudios Sociales', 'ES', 2, 5),
       (8, true, 3, 'Ciencias Naturales', 'CN', 3, 1),
       (8, true, 3, 'Matemáticas', 'Mat', 3, 5),
       (8, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (8, false, 3, 'Computación', 'COMP', 3, 7),
       (8, false, 3, 'Conducta', 'COND', 3, 8);


-- 6to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (9, true, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (9, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (9, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (9, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (9, false, 3, 'Educación Física', 'EF', 2, 2),
       (9, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (9, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (9, true, 3, 'Estudios Sociales', 'ES', 2, 5),
       (9, true, 3, 'Ciencias Naturales', 'CN', 3, 1),
       (9, true, 3, 'Matemáticas', 'Mat', 3, 5),
       (9, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (9, false, 3, 'Computación', 'COMP', 3, 7),
       (9, false, 3, 'Conducta', 'COND', 3, 8);

-- #####################################################################################

-- 7mo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (10, true, 3, 'Lengua y Literatura', 'L y L', 1, 1),
       (10, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2),
       (10, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3),
       (10, false, 3, 'Creciendo en Valores', 'C en V', 2, 1),
       (10, false, 3, 'Educación Física', 'EF', 2, 2),
       (10, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3),
       (10, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4),
       (10, true, 1, '(CS) Geografía', 'GEOG', 2, 5),
       (10, true, 2, '(CS) Historia', 'HIST', 2, 6),
       (10, true, 3, 'Ciencias Naturales', 'CN', 3, 1),
       (10, true, 3, 'Matemáticas', 'Mat', 3, 5),
       (10, false, 3, 'Educación Cristiana', 'EC', 3, 6),
       (10, false, 3, 'Computación', 'COMP', 3, 7),
       (10, false, 3, 'Conducta', 'COND', 3, 8);


-- 8vo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (11, true, 3, 'Lengua y Literatura', 'L y L',1,1),
       (11, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2),
       (11, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC',1,3),
       (11, false, 3, 'Creciendo en Valores', 'C en V',2,1),
       (11, false, 3, 'Educación Física', 'EF',2,2),
       (11, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3),
       (11, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4),
       (11, true, 1, '(CS) Geografía', 'GEOG',2,5),
       (11, true, 2, '(CS) Historia', 'HIST',2,6),
       (11, true, 3, 'Ciencias Naturales', 'CN',3,1),
       (11, true, 3, 'Matemáticas', 'Mat',3,5),
       (11, false, 3, 'Educación Cristiana', 'EC',3,6),
       (11, false, 3, 'Computación', 'COMP',3,7),
       (11, false, 3, 'Conducta', 'COND',3,8);


-- 9no grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (12, true, 3, 'Lengua y Literatura', 'L y L',1,1),
       (12, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2),
       (12, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC',1,3),
       (12, false, 3, 'Creciendo en Valores', 'C en V',2,1),
       (12, false, 3, 'Educación Física', 'EF',2,2),
       (12, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3),
       (12, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4),
       (12, true, 1, '(CS) Geografía', 'GEOG',2,5),
       (12, true, 2, '(CS) Historia', 'HIST',2,6),
       (12, true, 3, 'Ciencias Naturales', 'CN',3,1),
       (12, true, 3, 'Matemáticas', 'Mat',3,5),
       (12, false, 3, 'Educación Cristiana', 'EC',3,6),
       (12, false, 3, 'Computación', 'COMP',3,7),
       (12, false, 3, 'Conducta', 'COND',3,8);


-- 10mo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (13, true, 3, 'Lengua y Literatura', 'L y L',1,1),
       (13, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2),
       (13, false, 3, 'Creciendo en Valores', 'C en V',2,1),
       (13, false, 3, 'Educación Física', 'EF',2,2),
       (13, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3),
       (13, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4),
       (13, true, 1, '(CS) Geografía', 'GEOG',2,5),
       (13, true, 3, 'Química', 'QUIM',3,2),
       (13, true, 3, 'Física', 'FIS',3,3),
       (13, true, 3, 'Matemáticas', 'Mat',3,5),
       (13, false, 3, 'Educación Cristiana', 'EC',3,6),
       (13, false, 3, 'Computación', 'COMP',3,7),
       (13, false, 3, 'Conducta', 'COND',3,8);

-- 11vo grado ---
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number)
VALUES (14, true, 3, 'Lengua y Literatura', 'L y L',1,1),
       (14, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2),
       (14, false, 3, 'Creciendo en Valores', 'C en V',2,1),
       (14, false, 3, 'Educación Física', 'EF',2,2),
       (14, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3),
       (14, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4),
       (14, true, 1, '(CS) Sociología', 'SOC',2,7),
       (14, true, 2, '(CS) Filosofía', 'FIL',2,8),
       (14, true, 3, 'Física', 'FIS',3,3),
       (14, true, 3, 'Biología', 'BIOL',3,4),
       (14, true, 3, 'Matemáticas', 'Mat',3,5),
       (14, false, 3, 'Educación Cristiana', 'EC',3,6),
       (14, false, 3, 'Computación', 'COMP',3,7),
       (14, false, 3, 'Conducta', 'COND',3,8);