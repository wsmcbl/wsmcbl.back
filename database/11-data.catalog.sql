set datestyle to 'European';

insert into secretary.tariffcatalog(concept, amount, duedate, typeid, modality)
values ('Pago de matrícula', 0, null, 4, 3),
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
       ('Pago treceavo mes', 0, '28/12/2024', 1, 3),
       ('Pago buzo escolar', 0, null, 2, 3),
       ('Pago uniforme escolar', 0, null, 2, 3),
       ('Pago excursión', 0, null, 3, 3);

insert into secretary.degreecatalog(label, modality)
values ('Primer Nivel', 1),
       ('Segundo Nivel', 1),
       ('Tercer Nivel', 1),
       ('Primer Grado', 2),
       ('Segundo Grado', 2),
       ('Tercer Grado', 2),
       ('Cuarto Grado', 2),
       ('Quinto Grado', 2),
       ('Sexto Grado', 2),
       ('Septimo Grado', 3),
       ('Octavo Grado', 3),
       ('Noveno Grado', 3),
       ('Décimo Grado', 3),
       ('Undécimo Grado', 3);


-- #####################################################################################
    
-- I nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (1, false, 3, 'Lengua y Literatura', 'L y L'),
       (1, false, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (1, false, 3, 'Matemáticas', 'Mat');

-- II nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (2, false, 3, 'Lengua y Literatura', 'L y L'),
       (2, false, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (2, false, 3, 'Matemáticas', 'Mat');

-- III nivel -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (3, false, 3, 'Lengua y Literatura', 'L y L'),
       (3, false, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (3, false, 3, 'Matemáticas', 'Mat');

-- #####################################################################################
    
-- 1er grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (4, false, 3, 'Creciendo en Valores', 'C en V'),
       (4, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (4, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (4, false, 3, 'Educación Física', 'EF'),
       (4, false, 3, 'Lengua y Literatura', 'L y L'),
       (4, false, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (4, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (4, false, 3, 'Matemáticas', 'Mat'),
       (4, false, 3, 'Conociendo mi Mundo', 'CM'),
       (4, false, 3, 'Conducta', 'COND'),
       (4, false, 3, 'Educación Cristiana', 'EC'),
       (4, false, 3, 'Computación', 'COMP');


-- 2do grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (5, false, 3, 'Creciendo en Valores', 'C en V'),
       (5, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (5, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (5, false, 3, 'Educación Física', 'EF'),
       (5, false, 3, 'Lengua y Literatura', 'L y L'),
       (5, false, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (5, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (5, false, 3, 'Matemáticas', 'Mat'),
       (5, false, 3, 'Conociendo mi Mundo', 'CM'),
       (5, false, 3, 'Conducta', 'COND'),
       (5, false, 3, 'Educación Cristiana', 'EC'),
       (5, false, 3, 'Computación', 'COMP');


-- 3er grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (6, false, 3, 'Creciendo en Valores', 'C en V'),
       (6, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (6, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (6, true, 3, 'Estudios Sociales', 'ES'),
       (6, false, 3, 'Educación Física', 'EF'),
       (6, true, 3, 'Lengua y Literatura', 'L y L'),
       (6, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (6, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (6, true, 3, 'Matemáticas', 'Mat'),
       (6, true, 3, 'Ciencias Naturales', 'CN'),
       (6, false, 3, 'Conducta', 'COND'),
       (6, false, 3, 'Educación Cristiana', 'EC'),
       (6, false, 3, 'Computación', 'COMP');


-- 4to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (7, false, 3, 'Creciendo en Valores', 'C en V'),
       (7, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (7, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (7, true, 3, 'Estudios Sociales', 'ES'),
       (7, false, 3, 'Educación Física', 'EF'),
       (7, true, 3, 'Lengua y Literatura', 'L y L'),
       (7, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (7, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (7, true, 3, 'Matemáticas', 'Mat'),
       (7, true, 3, 'Ciencias Naturales', 'CN'),
       (7, false, 3, 'Conducta', 'COND'),
       (7, false, 3, 'Educación Cristiana', 'EC'),
       (7, false, 3, 'Computación', 'COMP');


-- 5to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (8, false, 3, 'Creciendo en Valores', 'C en V'),
       (8, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (8, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (8, true, 3, 'Estudios Sociales', 'ES'),
       (8, false, 3, 'Educación Física', 'EF'),
       (8, true, 3, 'Lengua y Literatura', 'L y L'),
       (8, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (8, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (8, true, 3, 'Matemáticas', 'Mat'),
       (8, true, 3, 'Ciencias Naturales', 'CN'),
       (8, false, 3, 'Conducta', 'COND'),
       (8, false, 3, 'Educación Cristiana', 'EC'),
       (8, false, 3, 'Computación', 'COMP');


-- 6to grado -- 
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (9, false, 3, 'Creciendo en Valores', 'C en V'),
       (9, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (9, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (9, true, 3, 'Estudios Sociales', 'ES'),
       (9, false, 3, 'Educación Física', 'EF'),
       (9, true, 3, 'Lengua y Literatura', 'L y L'),
       (9, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (9, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (9, true, 3, 'Matemáticas', 'Mat'),
       (9, true, 3, 'Ciencias Naturales', 'CN'),
       (9, false, 3, 'Conducta', 'COND'),
       (9, false, 3, 'Educación Cristiana', 'EC'),
       (9, false, 3, 'Computación', 'COMP');

-- #####################################################################################

-- 7mo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (10, false, 3, 'Creciendo en Valores', 'C en V'),
       (10, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (10, false, 3, 'Educación Física', 'EF'),
       (10, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (10, true, 1, '(CS) Geografía', 'GEOG'),
       (10, true, 2, '(CS) Historia', 'HIST'),
       (10, true, 3, 'Lengua y Literatura', 'L y L'),
       (10, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (10, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (10, true, 3, 'Ciencias Naturales', 'CN'),
       (10, true, 3, 'Matemáticas', 'Mat'),
       (10, false, 3, 'Conducta', 'COND'),
       (10, false, 3, 'Educación Cristiana', 'EC'),
       (10, false, 3, 'Computación', 'COMP');


-- 8vo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (11, false, 3, 'Creciendo en Valores', 'C en V'),
       (11, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (11, false, 3, 'Educación Física', 'EF'),
       (11, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (11, true, 1, '(CS) Geografía', 'GEOG'),
       (11, true, 2, '(CS) Historia', 'HIST'),
       (11, true, 3, 'Lengua y Literatura', 'L y L'),
       (11, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (11, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (11, true, 3, 'Ciencias Naturales', 'CN'),
       (11, true, 3, 'Matemáticas', 'Mat'),
       (11, false, 3, 'Conducta', 'COND'),
       (11, false, 3, 'Educación Cristiana', 'EC'),
       (11, false, 3, 'Computación', 'COMP');


-- 9no grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (12, false, 3, 'Creciendo en Valores', 'C en V'),
       (12, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (12, false, 3, 'Educación Física', 'EF'),
       (12, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (12, true, 1, '(CS) Geografía', 'GEOG'),
       (12, true, 2, '(CS) Historia', 'HIST'),
       (12, true, 3, 'Lengua y Literatura', 'L y L'),
       (12, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (12, false, 3, 'Taller de Arte y Cultura (Música)', 'TAC'),
       (12, true, 3, 'Ciencias Naturales', 'CN'),
       (12, true, 3, 'Matemáticas', 'Mat'),
       (12, false, 3, 'Conducta', 'COND'),
       (12, false, 3, 'Educación Cristiana', 'EC'),
       (12, false, 3, 'Computación', 'COMP');


-- 10mo grado --
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (13, false, 3, 'Creciendo en Valores', 'C en V'),
       (13, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (13, false, 3, 'Educación Física', 'EF'),
       (13, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (13, true, 1, '(CS) Geografía', 'GEOG'),
       (13, true, 3, 'Lengua y Literatura', 'L y L'),
       (13, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (13, true, 3, 'Química', 'QUIM'),
       (13, true, 3, 'Física', 'FIS'),
       (13, true, 3, 'Matemáticas', 'Mat'),
       (13, false, 3, 'Conducta', 'COND'),
       (13, false, 3, 'Educación Cristiana', 'EC'),
       (13, false, 3, 'Computación', 'COMP');

-- 11vo grado ---
INSERT INTO secretary.subjectcatalog(degreecatalogid, ismandatory, semester, name, initials)
VALUES (14, false, 3, 'Creciendo en Valores', 'C en V'),
       (14, false, 3, 'Derecho y Dignidad de las mujeres', 'DDM'),
       (14, false, 3, 'Educación Física', 'EF'),
       (14, false, 3, 'Educación para Aprender, Emprender, Prosperar', 'AEP'),
       (14, true, 1, '(CS) Sociología', 'SOC'),
       (14, true, 2, '(CS) Filosofía', 'FIL'),
       (14, true, 3, 'Lengua y Literatura', 'L y L'),
       (14, true, 3, 'Lengua Extranjera (Inglés)', 'Ing'),
       (14, true, 3, 'Física', 'FIS'),
       (14, true, 3, 'Biología', 'BIOL'),
       (14, true, 3, 'Matemáticas', 'Mat'),
       (14, false, 3, 'Conducta', 'COND'),
       (14, false, 3, 'Educación Cristiana', 'EC'),
       (14, false, 3, 'Computación', 'COMP');