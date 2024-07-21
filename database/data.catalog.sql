set datestyle to 'European';

insert into secretary.tariffcatalog(concept, amount, duedate, typeid, modality)
values ('Pago de matricula', 0, null, 3, 3),
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

insert into secretary.gradecatalog(gradelabel, modality)
values ('n1', 1),
       ('n2', 1),
       ('n3', 1),
       ('1ro', 2),
       ('2do', 2),
       ('3ro', 2),
       ('4to', 2),
       ('5to', 2),
       ('6to', 2),
       ('7mo', 3),
       ('8vo', 3),
       ('9no', 3),
       ('10mo', 3),
       ('11vo', 3);


-- #####################################################################################
    
-- I nivel -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (1, false, 3, 'Lengua y Literatura'),
       (1, false, 3, 'Lengua Extranjera (Inglés)'),
       (1, false, 3, 'Matemáticas');

-- II nivel -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (2, false, 3, 'Lengua y Literatura'),
       (2, false, 3, 'Lengua Extranjera (Inglés)'),
       (2, false, 3, 'Matemáticas');

-- III nivel -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (3, false, 3, 'Lengua y Literatura'),
       (3, false, 3, 'Lengua Extranjera (Inglés)'),
       (3, false, 3, 'Matemáticas');

-- #####################################################################################
    
-- 1er grado -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (4, false, 3, 'Creciendo en Valores'),
       (4, false, 3, 'Derecho y Dignidad de las mujeres'),
       (4, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (4, false, 3, 'Educación Física'),
       (4, false, 3, 'Lengua y Literatura'),
       (4, false, 3, 'Lengua Extranjera (Inglés)'),
       (4, false, 3, 'Taller de Arte y Cultura (Música)'),
       (4, false, 3, 'Matemáticas'),
       (4, false, 3, 'Conociendo mi Mundo'),
       (4, false, 3, 'Conducta'),
       (4, false, 3, 'Educación Cristiana'),
       (4, false, 3, 'Computación');


-- 2do grado -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (5, false, 3, 'Creciendo en Valores'),
       (5, false, 3, 'Derecho y Dignidad de las mujeres'),
       (5, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (5, false, 3, 'Educación Física'),
       (5, false, 3, 'Lengua y Literatura'),
       (5, false, 3, 'Lengua Extranjera (Inglés)'),
       (5, false, 3, 'Taller de Arte y Cultura (Música)'),
       (5, false, 3, 'Matemáticas'),
       (5, false, 3, 'Conociendo mi Mundo'),
       (5, false, 3, 'Conducta'),
       (5, false, 3, 'Educación Cristiana'),
       (5, false, 3, 'Computación');


-- 3er grado -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (6, false, 3, 'Creciendo en Valores'),
       (6, false, 3, 'Derecho y Dignidad de las mujeres'),
       (6, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (6, true, 3, 'Estudios Sociales'),
       (6, false, 3, 'Educación Física'),
       (6, true, 3, 'Lengua y Literatura'),
       (6, true, 3, 'Lengua Extranjera (Inglés)'),
       (6, false, 3, 'Taller de Arte y Cultura (Música)'),
       (6, true, 3, 'Matemáticas'),
       (6, true, 3, 'Ciencias Naturales'),
       (6, false, 3, 'Conducta'),
       (6, false, 3, 'Educación Cristiana'),
       (6, false, 3, 'Computación');


-- 4to grado -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (7, false, 3, 'Creciendo en Valores'),
       (7, false, 3, 'Derecho y Dignidad de las mujeres'),
       (7, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (7, true, 3, 'Estudios Sociales'),
       (7, false, 3, 'Educación Física'),
       (7, true, 3, 'Lengua y Literatura'),
       (7, true, 3, 'Lengua Extranjera (Inglés)'),
       (7, false, 3, 'Taller de Arte y Cultura (Música)'),
       (7, true, 3, 'Matemáticas'),
       (7, true, 3, 'Ciencias Naturales'),
       (7, false, 3, 'Conducta'),
       (7, false, 3, 'Educación Cristiana'),
       (7, false, 3, 'Computación');


-- 5to grado -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (8, false, 3, 'Creciendo en Valores'),
       (8, false, 3, 'Derecho y Dignidad de las mujeres'),
       (8, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (8, true, 3, 'Estudios Sociales'),
       (8, false, 3, 'Educación Física'),
       (8, true, 3, 'Lengua y Literatura'),
       (8, true, 3, 'Lengua Extranjera (Inglés)'),
       (8, false, 3, 'Taller de Arte y Cultura (Música)'),
       (8, true, 3, 'Matemáticas'),
       (8, true, 3, 'Ciencias Naturales'),
       (8, false, 3, 'Conducta'),
       (8, false, 3, 'Educación Cristiana'),
       (8, false, 3, 'Computación');


-- 6to grado -- 
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (9, false, 3, 'Creciendo en Valores'),
       (9, false, 3, 'Derecho y Dignidad de las mujeres'),
       (9, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (9, true, 3, 'Estudios Sociales'),
       (9, false, 3, 'Educación Física'),
       (9, true, 3, 'Lengua y Literatura'),
       (9, true, 3, 'Lengua Extranjera (Inglés)'),
       (9, false, 3, 'Taller de Arte y Cultura (Música)'),
       (9, true, 3, 'Matemáticas'),
       (9, true, 3, 'Ciencias Naturales'),
       (9, false, 3, 'Conducta'),
       (9, false, 3, 'Educación Cristiana'),
       (9, false, 3, 'Computación');

-- #####################################################################################

-- 7mo grado --
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (10, false, 3, 'Creciendo en Valores'),
       (10, false, 3, 'Derecho y Dignidad de las mujeres'),
       (10, false, 3, 'Educación Física'),
       (10, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (10, true, 1, '(CS) Geografía'),
       (10, true, 2, '(CS) Historia'),
       (10, true, 3, 'Lengua y Literatura'),
       (10, true, 3, 'Lengua Extranjera (Inglés)'),
       (10, false, 3, 'Taller de Arte y Cultura (Música)'),
       (10, true, 3, 'Ciencias Naturales'),
       (10, true, 3, 'Matemáticas'),
       (10, false, 3, 'Conducta'),
       (10, false, 3, 'Educación Cristiana'),
       (10, false, 3, 'Computación');


-- 8vo grado --
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (11, false, 3, 'Creciendo en Valores'),
       (11, false, 3, 'Derecho y Dignidad de las mujeres'),
       (11, false, 3, 'Educación Física'),
       (11, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (11, true, 1, '(CS) Geografía'),
       (11, true, 2, '(CS) Historia'),
       (11, true, 3, 'Lengua y Literatura'),
       (11, true, 3, 'Lengua Extranjera (Inglés)'),
       (11, false, 3, 'Taller de Arte y Cultura (Música)'),
       (11, true, 3, 'Ciencias Naturales'),
       (11, true, 3, 'Matemáticas'),
       (11, false, 3, 'Conducta'),
       (11, false, 3, 'Educación Cristiana'),
       (11, false, 3, 'Computación');


-- 9no grado --
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (12, false, 3, 'Creciendo en Valores'),
       (12, false, 3, 'Derecho y Dignidad de las mujeres'),
       (12, false, 3, 'Educación Física'),
       (12, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (12, true, 1, '(CS) Geografía'),
       (12, true, 2, '(CS) Historia'),
       (12, true, 3, 'Lengua y Literatura'),
       (12, true, 3, 'Lengua Extranjera (Inglés)'),
       (12, false, 3, 'Taller de Arte y Cultura (Música)'),
       (12, true, 3, 'Ciencias Naturales'),
       (12, true, 3, 'Matemáticas'),
       (12, false, 3, 'Conducta'),
       (12, false, 3, 'Educación Cristiana'),
       (12, false, 3, 'Computación');


-- 10mo grado --
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (13, false, 3, 'Creciendo en Valores'),
       (13, false, 3, 'Derecho y Dignidad de las mujeres'),
       (13, false, 3, 'Educación Física'),
       (13, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (13, true, 1, '(CS) Geografía'),
       (13, true, 3, 'Lengua y Literatura'),
       (13, true, 3, 'Lengua Extranjera (Inglés)'),
       (13, true, 3, 'Química'),
       (13, true, 3, 'Física'),
       (13, true, 3, 'Matemáticas'),
       (13, false, 3, 'Conducta'),
       (13, false, 3, 'Educación Cristiana'),
       (13, false, 3, 'Computación');

-- 11vo grado ---
INSERT INTO secretary.subjectcatalog(gradecatalogid, ismandatory, semester, name)
VALUES (14, false, 3, 'Creciendo en Valores'),
       (14, false, 3, 'Derecho y Dignidad de las mujeres'),
       (14, false, 3, 'Educación Física'),
       (14, false, 3, 'Educación para Aprender, Emprender, Prosperar'),
       (14, true, 1, '(CS) Sociología'),
       (14, true, 2, '(CS) Filosofía'),
       (14, true, 3, 'Lengua y Literatura'),
       (14, true, 3, 'Lengua Extranjera (Inglés)'),
       (14, true, 3, 'Física'),
       (14, true, 3, 'Biología'),
       (14, true, 3, 'Matemáticas'),
       (14, false, 3, 'Conducta'),
       (14, false, 3, 'Educación Cristiana'),
       (14, false, 3, 'Computación');