create schema if not exists Accounting;

create table if not exists Accounting.Discount
(
    discountId smallint primary key,
    description varchar(200) not null,
    amount float not null,
    tag varchar(50)
);

create table if not exists Accounting.Student
(
    studentId varchar(20) primary key,
    discountId smallint not null,
    educationalLevel smallint not null,
    foreign key (studentId) references Secretary.Student,
    foreign key (discountId) references Accounting.Discount
);

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
    schoolYear varchar(20) not null,
    educationalLeve smallint not null,
    concept varchar(200) not null,
    amount float not null,
    dueDate date,
    late boolean,
    typeId int not null,
    foreign key (typeId) references Accounting.TariffType
);

create table  if not exists Accounting.Transaction
(
    transactionId varchar(20) primary key default accounting.generate_transaction_id(),
    total float not null,
    date timestamp with time zone not null,
    studentId varchar(15) not null,
    cashierId varchar(15) not null,
    foreign key (studentId) references Accounting.Student,
    foreign key (cashierId) references Accounting.Cashier
);

create table if not exists Accounting.Transaction_Tariff
(
    transactionId varchar(15) not null,
    tariffId int not null,
    amount float not null,
    primary key (transactionId, tariffId),
    foreign key (transactionId) references Accounting.Transaction,
    foreign key (tariffId) references Accounting.Tariff
);

create table if not exists Accounting.DebtHistory
(
    studentId varchar(20) not null,
    tariffId int not null,
    schoolyear varchar(20) not null,
    subAmount float not null,
    arrear float not null,
    amount float not null GENERATED ALWAYS as (subAmount + arrear) stored,
    debtBalance float,
    isPaid bool not null,
    primary key (studentId, tariffId),
    foreign key (studentId) references Accounting.Student,
    foreign key (tariffId) references Accounting.Tariff,
    foreign key (schoolyear) references Secretary.Schoolyear
);