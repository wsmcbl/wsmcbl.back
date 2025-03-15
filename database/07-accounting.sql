create schema if not exists Accounting;

create table if not exists Accounting.Discount
(
    discountId smallint primary key,
    description varchar(200) not null,
    tag varchar(50)
);

create table if not exists Accounting.DiscountEducationalLevel
(
    del serial primary key,
    discountId int not null,
    educationalLevel smallint not null,
    amount float not null,
    foreign key (discountId) references Accounting.Discount
);

create table if not exists Accounting.Student
(
    studentId varchar(20) primary key,
    discountel int not null,
    educationalLevel smallint not null,
    foreign key (studentId) references Secretary.Student,
    foreign key (discountel) references Accounting.DiscountEducationalLevel
);

create table  if not exists Accounting.Cashier
(
    cashierId varchar(15) primary key default accounting.generate_cashier_id(),
    userId uuid not null,
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
    educationalLevel smallint not null,
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
    number int default NEXTVAL('accounting.transaction_number_seq'),
    total float not null,
    date timestamp with time zone not null,
    isValid bool default true not null,
    studentId varchar(15) not null,
    cashierId varchar(15) not null,
    foreign key (studentId) references Accounting.Student,
    foreign key (cashierId) references Accounting.Cashier
);

create index IDX_date ON Accounting.Transaction (date);

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

create table if not exists Accounting.ExchangeRate
(
    rateId serial not null primary key,
    schoolyear varchar(20) not null,
    value decimal(18, 2) not null,
    foreign key (schoolyear) references secretary.schoolyear
);