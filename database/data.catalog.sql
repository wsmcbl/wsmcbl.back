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

-- Para gradecatalogid = 10
INSERT INTO secretary.subjectcatalog(gradecatalogid, name, ismandatory)
VALUES
    (10, 'Lengua y literatura', true),
    (10, 'Matemáticas', true),
    (10, 'Inglés', true),
    (10, 'Ciencias naturales', true),
    (10, 'Química', true),
    (10, 'Biología', true),
    (10, 'Física', true),
    (10, 'Estudios sociales', true),
    (10, 'Geografía', true),
    (10, 'Historia', true),
    (10, 'Creciendo en valores', true),
    (10, 'Computación', true),
    (10, 'Aprender, Emprender, Prosperar', true),
    (10, 'Conducta', true),
    (10, 'Educación física', true);

-- Para gradecatalogid = 11
INSERT INTO secretary.subjectcatalog(gradecatalogid, name, ismandatory)
VALUES
    (11, 'Lengua y literatura', true),
    (11, 'Matemáticas', true),
    (11, 'Inglés', true),
    (11, 'Ciencias naturales', true),
    (11, 'Química', true),
    (11, 'Biología', true),
    (11, 'Física', true),
    (11, 'Estudios sociales', true),
    (11, 'Geografía', true),
    (11, 'Historia', true),
    (11, 'Creciendo en valores', true),
    (11, 'Computación', true),
    (11, 'Aprender, Emprender, Prosperar', true),
    (11, 'Conducta', true),
    (11, 'Educación física', true);

-- Para gradecatalogid = 12
INSERT INTO secretary.subjectcatalog(gradecatalogid, name, ismandatory)
VALUES
    (12, 'Lengua y literatura', true),
    (12, 'Matemáticas', true),
    (12, 'Inglés', true),
    (12, 'Ciencias naturales', true),
    (12, 'Química', true),
    (12, 'Biología', true),
    (12, 'Física', true),
    (12, 'Estudios sociales', true),
    (12, 'Geografía', true),
    (12, 'Historia', true),
    (12, 'Creciendo en valores', true),
    (12, 'Computación', true),
    (12, 'Aprender, Emprender, Prosperar', true),
    (12, 'Conducta', true),
    (12, 'Educación física', true);

-- Para gradecatalogid = 13
INSERT INTO secretary.subjectcatalog(gradecatalogid, name, ismandatory)
VALUES
    (13, 'Lengua y literatura', true),
    (13, 'Matemáticas', true),
    (13, 'Inglés', true),
    (13, 'Ciencias naturales', true),
    (13, 'Química', true),
    (13, 'Biología', true),
    (13, 'Física', true),
    (13, 'Estudios sociales', true),
    (13, 'Geografía', true),
    (13, 'Historia', true),
    (13, 'Creciendo en valores', true),
    (13, 'Computación', true),
    (13, 'Aprender, Emprender, Prosperar', true),
    (13, 'Conducta', true),
    (13, 'Educación física', true);

-- Para gradecatalogid = 14
INSERT INTO secretary.subjectcatalog(gradecatalogid, name, ismandatory)
VALUES
    (14, 'Lengua y literatura', true),
    (14, 'Matemáticas', true),
    (14, 'Inglés', true),
    (14, 'Ciencias naturales', true),
    (14, 'Química', true),
    (14, 'Biología', true),
    (14, 'Física', true),
    (14, 'Estudios sociales', true),
    (14, 'Geografía', true),
    (14, 'Historia', true),
    (14, 'Creciendo en valores', true),
    (14, 'Computación', true),
    (14, 'Aprender, Emprender, Prosperar', true),
    (14, 'Conducta', true),
    (14, 'Educación física', true);