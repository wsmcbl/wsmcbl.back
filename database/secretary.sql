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
    birthday date not null,
    enrollmentLabel varchar(20)
);

create table if not exists Secretary.Schoolyear
(
    schoolyearId varchar(15) primary key,
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


create table if not exists Secretary.Grade
(
    gradeId serial primary key ,
    gradeLabel varchar(25) not null,
    schoolYear varchar(15) not null,
    foreign key (schoolYear) references Secretary.Schoolyear
);

create table if not exists Secretary.Subject
(
    subjectId varchar(15) primary key,
    gradeId int not null,
    name varchar(100) not null,
    foreign key (gradeId) references Secretary.Grade
);