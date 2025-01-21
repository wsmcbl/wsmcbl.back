create schema if not exists Config;

create table if not exists Config.Role
(
    roleId serial primary key not null,
    name varchar(50) not null,
    description varchar(150) not null
);


create table if not exists Config.User
(
    userId uuid primary key DEFAULT uuid_generate_v4(),
    roleId int not null,
    name varchar(50) not null,
    secondName varchar(50),
    surname varchar(50) not null,
    secondSurname varchar(50),
    email varchar(100) unique not null,
    password varchar(100) not null,
    userState boolean not null,
    createdAt timestamp with time zone not null,
    updatedAt timestamp with time zone not null,
    foreign key (roleId) references config.Role
);

create unique index IDX_email ON config.User (email);

create table if not exists Config.Permission
(
    permissionId serial primary key not null,
    group varchar(50) not null,
    name varchar(50) not null,
    spanishName varchar(50) not null,
    description varchar(150) not null
);

create table if not exists Config.User_Permission
(
    userId uuid not null,
    permissionId int not null,
    foreign key (userId) references Config.User,
    foreign key (permissionId) references Config.Permission
);

create table if not exists Config.Role_Permission
(
    roleId int not null,
    permissionId int not null,
    foreign key (roleId) references Config.Role,
    foreign key (permissionId) references Config.Permission
);