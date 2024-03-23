-- create database wsmcbl.database;

create schema Academy;

create table if not exists Academy.Enrollment
(
    enrollmentId varchar(20) primary key,
    grade numeric not null,
    section varchar(2) not null
);


create schema Accounting;

create table if not exists Accounting.Student
(
    studentId varchar(20) primary key,
    enrollmentId varchar(20),
    name varchar(50) not null,
    secondName varchar(50),
    surname varchar(50) not null ,
    secondSurname varchar(50),
    schoolyear varchar(4) not null,
    tutor varchar(100),
    foreign key (enrollmentId) references Academy.Enrollment
);

create table  if not exists Accounting.Cashier
(
    cashierId varchar(100) primary key,
    name varchar(100) not null,
    surname varchar(100) not null,
    sex bool
);

create table  if not exists Accounting.Tariff
(
    tariffId serial unique primary key,
    concept varchar(100) not null,
    amount float not null
);

create table  if not exists Accounting.Transaction
(
    transactionId varchar(100) primary key,
    studentId varchar(100) not null,
    cashierId varchar(100) not null,
    total float not null,
    date date not null,
    discount float,
    foreign key (studentId) references Accounting.Student
);

create table  if not exists Accounting.Tariff_Transaction
(
    transactionId varchar(100) not null,
    tariffId serial not null,
    foreign key (transactionId) references Accounting.Transaction,
    foreign key (tariffId) references Accounting.Tariff
)