create schema if not exists Config;

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
    rolId varchar(15) primary key,
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