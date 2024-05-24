-- drop database wsmcbl_database;
-- create database wsmcbl_database;
    

create schema Academy;
create schema Accounting;
create schema Secretary;
create schema Config;


--- ############################## ---

create table if not exists Config.User
(
    userId varchar(15) primary key,
    name varchar(50) not null,
    secondName varchar(50),
    surname varchar(50) not null,
    secondSurname varchar(50),
    userName varchar(45) not null,
    password varchar(100) not null,
    email varchar(100),
    userState boolean not null
);

create table if not exists Config.Rol
(
    rolId varchar(15) primary key ,
    nameRol varchar(50) not null
);

create table if not exists Config.Permission
(
    permissionId varchar(15) primary key,
    name varchar(50) not null
);

create table if not exists Config.User_Rol
(
    userId varchar(15) not null,
    rolId varchar(15) not null,
    foreign key (userId) references Config.User,
    foreign key (rolId) references Config.Rol
);

create table if not exists Config.Rol_Permission
(
    rolId varchar(15) not null,
    permissionId varchar(15) not null
);


--- ############################## ---


create table if not exists Academy.Grade
(
    gradeId varchar(15) primary key,
    schoolYear varchar(4) not null
);

create table if not exists Academy.Enrollment
(
    enrollmentId varchar(20) primary key,
    enrollmentLabel varchar(20) not null,
    section varchar(2) not null,
    schoolYear varchar(4) not null,
    capacity smallint,
    quantity smallint,
    gradeId varchar(15) not null,
    foreign key (gradeId) references Academy.Grade
);

create table if not exists Academy.Teacher
(
    teacherId varchar(15) primary key,
    enrollmentId varchar(20),
    userId varchar(15) not null,
    foreign key (enrollmentId) references Academy.Enrollment,
    foreign key (userId) references Config.User
);

create table if not exists Academy.Teacher_Enrollment
(
    id varchar(15) primary key,
    teacherId varchar(15) not null,
    subjectId varchar(15) not null,
    enrollmentId varchar(15) not null,
    foreign key (teacherId) references Academy.Teacher,
    foreign key (enrollmentId) references Academy.Teacher 
);

create table if not exists Academy.Subject
(
    subjectId varchar(15) primary key,
    name varchar(100) not null,
    gradeId varchar(15) not null,
    foreign key (gradeId) references Academy.Grade
);

--- ############################## ---

create table if not exists Secretary.Student
(
    studentId varchar(20) primary key,
    name varchar(50) not null,
    secondName varchar(50),
    surname varchar(50) not null,
    secondSurname varchar(50),
    studentState boolean not null,
    schoolyear varchar(4) not null,
    tutor varchar(100),
    sex boolean not null,
    birthday date not null,
    enrollmentLabel varchar(20),
    discountId smallint
);

create table if not exists Secretary.Student_Enrollment
(
    id varchar(15) primary key,
    schoolYear varchar(4) not null,
    studentId varchar(15) not null,
    enrollmentId varchar(15) not null,
    foreign key (studentId) references Secretary.Student,
    foreign key (enrollmentId) references Academy.Enrollment
);

--- ############################## ---

create table  if not exists Accounting.Cashier
(
    cashierId varchar(15) primary key,
    userId varchar(15) not null,
    foreign key (userId) references Config.User
);

create table if not exists Accounting.TariffType
(
    typeId serial unique primary key,
    description varchar(50) not null
);

create table if not exists Accounting.Tariff
(
    tariffId serial unique primary key,
    schoolYear varchar(4) not null,
    concept varchar(200) not null,
    amount float not null,
    dueDate date,
    late boolean,
    typeId int not null,
    foreign key (typeId) references Accounting.TariffType
);

create table  if not exists Accounting.Transaction
(
    transactionId varchar(20) primary key,
    total float not null,
    date timestamp with time zone not null,
    studentId varchar(15) not null,
    cashierId varchar(15) not null,
    foreign key (studentId) references Secretary.Student,
    foreign key (cashierId) references Accounting.Cashier
);

create table if not exists Accounting.Transaction_Tariff
(
    transactionId varchar(15) not null,
    tariffId int not null,
    amount float not null,
    discount float,
    arrears float,
    subTotal float not null,
    primary key (transactionId, tariffId),
    foreign key (transactionId) references Accounting.Transaction,
    foreign key (tariffId) references Accounting.Tariff
);

create table if not exists Accounting.Discount
(
    discountId smallint primary key,
    description varchar(200) not null,
    amount float not null,
    tag varchar(50)
);