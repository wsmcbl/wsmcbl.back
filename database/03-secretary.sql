create schema if not exists Secretary;

create table if not exists Secretary.StudentTutor
(
    tutorId varchar(15) primary key default secretary.generate_tutor_id(),
    name varchar(70) not null,
    phone varchar(100) not null,
    email varchar(100)
);

create table if not exists Secretary.Student
(
    studentId varchar(20) primary key,
    tutorId varchar(15) not null,
    name varchar(50) not null,
    secondName varchar(100),
    surname varchar(50) not null,
    secondSurname varchar(50),
    studentState boolean not null,
    sex boolean not null,
    birthday date not null,
    diseases varchar(200) not null,
    religion varchar(100) not null,
    address varchar(500) not null,
    minedId varchar(30) null,
    profileImage bytea null CHECK (LENGTH(profileImage) <= 1000000),
    accessToken varchar(20) null,
    foreign key (tutorId) references Secretary.StudentTutor
);

create table if not exists Secretary.StudentParent
(
    parentId varchar(15) primary key default secretary.generate_parent_id(),
    studentId varchar(15) not null,
    sex bool not null,
    name varchar(70) not null,
    idCard varchar(25),
    occupation varchar(150),
    foreign key (studentId) references Secretary.Student
);

create table if not exists Secretary.StudentFile
(
    fileId serial primary key,
    studentId varchar(15) not null,
    transferSheet boolean not null,
    birthDocument boolean not null,
    parentIdentifier boolean not null,
    updatedGradeReport boolean not null,
    conductDocument boolean not null,
    financialSolvency boolean not null,
    foreign key (studentId) references Secretary.Student
);

create table if not exists Secretary.StudentMeasurements
(
    measurementId serial primary key,
    studentId varchar(15) not null,
    weight float not null,
    height int not null,
    foreign key (studentId) references Secretary.Student
);


create table if not exists Secretary.Schoolyear
(
    schoolyearId varchar(15) primary key default secretary.generate_schoolyear_id(),
    label varchar(15) not null,
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

create table if not exists Secretary.Degree
(
    degreeId varchar(25) primary key default secretary.generate_degree_id(),
    label varchar(25) not null,
    tag varchar(20) not null,
    schoolYear varchar(15) not null,
    educationalLevel varchar(50) not null,
    quantity int not null,
    foreign key (schoolYear) references Secretary.Schoolyear
);

create table if not exists Secretary.SubjectArea
(
    areaId serial not null primary key,
    name varchar(500) not null
);

create table if not exists Secretary.Subject
(
    subjectId varchar(15) primary key default secretary.generate_subject_id(),
    degreeId varchar(25) not null,
    areaId int not null,
    name varchar(100) not null,
    isMandatory boolean not null,
    semester int not null,
    initials varchar(10) not null,
    number int not null,
    foreign key (degreeId) references Secretary.Degree,
    foreign key (areaId) references Secretary.SubjectArea
);

create table if not exists Secretary.DegreeCatalog
(
    degreeCatalogId serial primary key,
    label varchar(50) not null,
    tag varchar(20) not null,
    educationalLevel int not null
);

create table if not exists Secretary.SubjectCatalog
(
    subjectCatalogId serial primary key,
    degreeCatalogId int not null,
    areaId int not null,
    name varchar(100) not null,
    isMandatory boolean not null,
    semester int not null,
    initials varchar(10) not null,
    number int not null,
    isActive boolean not null,
    foreign key (degreeCatalogId) references Secretary.DegreeCatalog,
    foreign key (areaId) references Secretary.SubjectArea
);

create table if not exists Secretary.TariffCatalog
(
    tariffCatalogId serial primary key,
    educationalLevel smallint not null,
    concept varchar(100) not null,
    amount decimal not null,
    dueDate date,
    typeId int not null,
    isActive boolean not null
);