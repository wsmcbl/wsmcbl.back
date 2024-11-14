create schema if not exists Config;

create table if not exists Config.User
(
    userId uuid primary key DEFAULT uuid_generate_v4(),
    name varchar(50) not null,
    secondName varchar(50),
    surname varchar(50) not null,
    secondSurname varchar(50),
    email varchar(100) unique not null,
    password varchar(100) not null,
    userState boolean not null,
    createdAt timestamp with time zone not null,
    updatedAt timestamp with time zone not null
);

CREATE UNIQUE INDEX IDX_email ON config.user (email);

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
    userId uuid not null,
    rolId varchar(15) not null,
    foreign key (userId) references Config.User,
    foreign key (rolId) references Config.Rol
);

create table if not exists Config.Rol_Permission
(
    rolId varchar(15) not null,
    permissionId varchar(15) not null
);