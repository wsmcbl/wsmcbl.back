set datestyle to 'European';

insert into secretary.subjectarea(name)
values ('Desarrollo de las habilidades de la comunicación y Talento Artístico y Cultural'),
       ('Desarrollo Personal, Social y Emocional'),
       ('Desarrollo del Pensamiento Lógico y Científico');


insert into secretary.tariffcatalog(concept, amount, duedate, typeid, educationallevel, isactive)
values ('Pago de matrícula', 0, null, 2, 3, true),
       ('Pago mes febrero', 0, '28/02/2024', 1, 3, true),
       ('Pago mes marzo', 0, '28/03/2024', 1, 3, true),
       ('Pago mes abril', 0, '28/04/2024', 1, 3, true),
       ('Pago mes mayo', 0, '28/05/2024', 1, 3, true),
       ('Pago mes junio', 0, '28/06/2024', 1, 3, true),
       ('Pago mes julio', 0, '28/07/2024', 1, 3, true),
       ('Pago mes agosto', 0, '28/08/2024', 1, 3, true),
       ('Pago mes septiembre', 0, '28/09/2024', 1, 3, true),
       ('Pago mes octubre', 0, '28/10/2024', 1, 3, true),
       ('Pago mes noviembre', 0, '28/11/2024', 1, 3, true),
       ('Pago mes diciembre', 0, '28/12/2024', 1, 3, true),
       ('Pago treceavo mes', 0, '28/12/2024', 1, 3, true);

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
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (1, false, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (1, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (1, false, 3, 'Matemáticas', 'Mat', 3, 1, true);

-- II nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (2, false, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (2, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (2, false, 3, 'Matemáticas', 'Mat', 3, 1, true);

-- III nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (3, false, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (3, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (3, false, 3, 'Matemáticas', 'Mat', 3, 1, true);

-- #####################################################################################

-- 1er grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (4, false, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (4, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (4, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (4, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (4, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (4, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (4, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (4, false, 3, 'Conociendo mi Mundo', 'CM', 2, 5, true),
       (4, false, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (4, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (4, false, 3, 'Computación', 'COMP', 3, 7, true),
       (4, false, 3, 'Conducta', 'COND', 3, 8, true);


-- 2do grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (5, false, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (5, false, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (5, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (5, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (5, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (5, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (5, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (5, false, 3, 'Conociendo mi Mundo', 'CM', 2, 5, true),
       (5, false, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (5, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (5, false, 3, 'Computación', 'COMP', 3, 7, true),
       (5, false, 3, 'Conducta', 'COND', 3, 8, true);


-- 3er grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (6, true, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (6, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (6, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (6, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (6, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (6, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (6, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (6, true, 3, 'Estudios Sociales', 'ES', 2, 5, true),
       (6, true, 3, 'Ciencias Naturales', 'CN', 3, 1, true),
       (6, true, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (6, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (6, false, 3, 'Computación', 'COMP', 3, 7, true),
       (6, false, 3, 'Conducta', 'COND', 3, 8, true);


-- 4to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (7, true, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (7, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (7, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (7, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (7, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (7, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (7, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (7, true, 3, 'Estudios Sociales', 'ES', 2, 5, true),
       (7, true, 3, 'Ciencias Naturales', 'CN', 3, 1, true),
       (7, true, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (7, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (7, false, 3, 'Computación', 'COMP', 3, 7, true),
       (7, false, 3, 'Conducta', 'COND', 3, 8, true);


-- 5to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (8, true, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (8, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (8, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (8, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (8, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (8, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (8, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (8, true, 3, 'Estudios Sociales', 'ES', 2, 5, true),
       (8, true, 3, 'Ciencias Naturales', 'CN', 3, 1, true),
       (8, true, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (8, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (8, false, 3, 'Computación', 'COMP', 3, 7, true),
       (8, false, 3, 'Conducta', 'COND', 3, 8, true);


-- 6to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (9, true, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (9, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (9, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (9, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (9, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (9, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (9, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (9, true, 3, 'Estudios Sociales', 'ES', 2, 5, true),
       (9, true, 3, 'Ciencias Naturales', 'CN', 3, 1, true),
       (9, true, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (9, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (9, false, 3, 'Computación', 'COMP', 3, 7, true),
       (9, false, 3, 'Conducta', 'COND', 3, 8, true);

-- #####################################################################################

-- 7mo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (10, true, 3, 'Lengua y Literatura', 'L y L', 1, 1, true),
       (10, true, 3, 'Lengua Extranjera (Inglés)', 'Ing', 1, 2, true),
       (10, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC', 1, 3, true),
       (10, false, 3, 'Creciendo en Valores', 'C en V', 2, 1, true),
       (10, false, 3, 'Educación Física', 'EF', 2, 2, true),
       (10, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP', 2, 3, true),
       (10, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM', 2, 4, true),
       (10, true, 1, '(CS) Geografía', 'GEOG', 2, 5, true),
       (10, true, 2, '(CS) Historia', 'HIST', 2, 6, true),
       (10, true, 3, 'Ciencias Naturales', 'CN', 3, 1, true),
       (10, true, 3, 'Matemáticas', 'Mat', 3, 5, true),
       (10, false, 3, 'Educación Cristiana', 'EC', 3, 6, true),
       (10, false, 3, 'Computación', 'COMP', 3, 7, true),
       (10, false, 3, 'Conducta', 'COND', 3, 8, true);


-- 8vo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (11, true, 3, 'Lengua y Literatura', 'L y L',1,1, true),
       (11, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2, true),
       (11, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC',1,3, true),
       (11, false, 3, 'Creciendo en Valores', 'C en V',2,1, true),
       (11, false, 3, 'Educación Física', 'EF',2,2, true),
       (11, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3, true),
       (11, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4, true),
       (11, true, 1, '(CS) Geografía', 'GEOG',2,5, true),
       (11, true, 2, '(CS) Historia', 'HIST',2,6, true),
       (11, true, 3, 'Ciencias Naturales', 'CN',3,1, true),
       (11, true, 3, 'Matemáticas', 'Mat',3,5, true),
       (11, false, 3, 'Educación Cristiana', 'EC',3,6, true),
       (11, false, 3, 'Computación', 'COMP',3,7, true),
       (11, false, 3, 'Conducta', 'COND',3,8, true);


-- 9no grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (12, true, 3, 'Lengua y Literatura', 'L y L',1,1, true),
       (12, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2, true),
       (12, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC',1,3, true),
       (12, false, 3, 'Creciendo en Valores', 'C en V',2,1, true),
       (12, false, 3, 'Educación Física', 'EF',2,2, true),
       (12, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3, true),
       (12, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4, true),
       (12, true, 1, '(CS) Geografía', 'GEOG',2,5, true),
       (12, true, 2, '(CS) Historia', 'HIST',2,6, true),
       (12, true, 3, 'Ciencias Naturales', 'CN',3,1, true),
       (12, true, 3, 'Matemáticas', 'Mat',3,5, true),
       (12, false, 3, 'Educación Cristiana', 'EC',3,6, true),
       (12, false, 3, 'Computación', 'COMP',3,7, true),
       (12, false, 3, 'Conducta', 'COND',3,8, true);


-- 10mo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (13, true, 3, 'Lengua y Literatura', 'L y L',1,1, true),
       (13, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2, true),
       (13, false, 3, 'Creciendo en Valores', 'C en V',2,1, true),
       (13, false, 3, 'Educación Física', 'EF',2,2, true),
       (13, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3, true),
       (13, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4, true),
       (13, true, 1, '(CS) Geografía', 'GEOG',2,5, true),
       (13, true, 3, 'Química', 'QUIM',3,2, true),
       (13, true, 3, 'Física', 'FIS',3,3, true),
       (13, true, 3, 'Matemáticas', 'Mat',3,5, true),
       (13, false, 3, 'Educación Cristiana', 'EC',3,6, true),
       (13, false, 3, 'Computación', 'COMP',3,7, true),
       (13, false, 3, 'Conducta', 'COND',3,8, true);

-- 11vo grado ---
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials, areaid, number, isactive)
VALUES (14, true, 3, 'Lengua y Literatura', 'L y L',1,1, true),
       (14, true, 3, 'Lengua Extranjera (Inglés)', 'Ing',1,2, true),
       (14, false, 3, 'Creciendo en Valores', 'C en V',2,1, true),
       (14, false, 3, 'Educación Física', 'EF',2,2, true),
       (14, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP',2,3, true),
       (14, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM',2,4, true),
       (14, true, 1, '(CS) Sociología', 'SOC',2,7, true),
       (14, true, 2, '(CS) Filosofía', 'FIL',2,8, true),
       (14, true, 3, 'Física', 'FIS',3,3, true),
       (14, true, 3, 'Biología', 'BIOL',3,4, true),
       (14, true, 3, 'Matemáticas', 'Mat',3,5, true),
       (14, false, 3, 'Educación Cristiana', 'EC',3,6, true),
       (14, false, 3, 'Computación', 'COMP',3,7, true),
       (14, false, 3, 'Conducta', 'COND',3,8, true);