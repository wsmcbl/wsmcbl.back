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