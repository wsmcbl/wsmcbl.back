create schema if not exists Secretary;


create table if not exists Secretary.Student
(
    studentId varchar(20) primary key,
    name varchar(50) not null,
    secondName varchar(50),
    surname varchar(50) not null,
    secondSurname varchar(50),
    studentState boolean not null,
    schoolyear varchar(15) not null,
    tutor varchar(100),
    sex boolean not null,
    birthday date not null
);



-- Generate secretary.grade id
CREATE SEQUENCE if not exists secretary.schoolyear_id_seq START 10;

CREATE OR REPLACE FUNCTION secretary.generate_schoolyear_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(3);
BEGIN
    seq_part := LPAD(NEXTVAL('secretary.schoolyear_id_seq')::TEXT, 3, '0');

    Return 'sch' || seq_part;
END;
$$ LANGUAGE plpgsql;

create table if not exists Secretary.Schoolyear
(
    schoolyearId varchar(15) primary key default secretary.generate_schoolyear_id(),
    label varchar(100) not null,
    startDate date not null,
    deadline date not null,
    isActive bool not null
);

create table if not exists Secretary.Schoolyear_Student
(
    schoolyear varchar(15) not null,
    studentId varchar(20) not null,
    primary key (schoolyear, studentId),
    foreign key (schoolyear) references Secretary.Schoolyear,
    foreign key (studentId) references Secretary.Student
);


-- Generate secretary.grade id
CREATE SEQUENCE if not exists secretary.grade_id_seq START 10;

CREATE OR REPLACE FUNCTION secretary.generate_grade_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(5);
BEGIN
    seq_part := LPAD(NEXTVAL('secretary.grade_id_seq')::TEXT, 5, '0');
    
    Return 'gd' || seq_part;
END;
$$ LANGUAGE plpgsql;

create table if not exists Secretary.Grade
(
    gradeId varchar(25) primary key default secretary.generate_grade_id(),
    gradeLabel varchar(25) not null,
    schoolYear varchar(15) not null,
    modality varchar(50) not null,
    quantity int not null,
    foreign key (schoolYear) references Secretary.Schoolyear
);



-- Generate secretary.subject id
CREATE SEQUENCE if not exists secretary.subject_id_seq START 10;

CREATE OR REPLACE FUNCTION secretary.generate_subject_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(6);
BEGIN
    seq_part := LPAD(NEXTVAL('secretary.subject_id_seq')::TEXT, 6, '0');

    Return 'sub' || seq_part;
END;
$$ LANGUAGE plpgsql;

create table if not exists Secretary.Subject
(
    subjectId varchar(15) primary key default secretary.generate_subject_id(),
    gradeId varchar(25) not null,
    name varchar(100) not null,
    isMandatory boolean not null,
    foreign key (gradeId) references Secretary.Grade
);





create table if not exists Secretary.GradeCatalog
(
    gradeCatalogId serial primary key ,
    gradeLabel varchar(50) not null,
    modality int not null
);

create table if not exists Secretary.SubjectCatalog
(
    subjectCatalogId serial primary key,
    gradeCatalogId int not null,
    name varchar(100) not null,
    isMandatory boolean not null,
    foreign key (gradeCatalogId) references Secretary.GradeCatalog
);

create table if not exists Secretary.TariffCatalog
(
    tariffCatalogId serial primary key,
    concept varchar(200) not null,
    amount float not null,
    dueDate date,
    typeId int not null,
    modality int not null
);