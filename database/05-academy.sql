create schema if not exists Academy;

create table if not exists Academy.Teacher
(
    teacherId varchar(15) primary key default academy.generate_teacher_id(),
    userId uuid not null,
    isGuide boolean not null,
    foreign key (userId) references Config.User
);

create table if not exists Academy.Enrollment
(
    enrollmentId varchar(15) primary key default academy.generate_enrollment_id(),
    degreeId varchar(25) not null,
    teacherId varchar(15) null,
    label varchar(20) not null,
    tag varchar(20) not null,
    schoolYear varchar(20) not null,
    section varchar(50) not null,
    capacity smallint,
    quantity smallint,
    foreign key (degreeId) references secretary.Degree,
    foreign key (schoolYear) references Secretary.Schoolyear,
    foreign key (teacherId) references Academy.Teacher
);

create table if not exists Academy.Subject
(
    subjectId varchar(15) not null,
    enrollmentId varchar(15) not null,
    teacherId varchar(15) null,
    primary key (subjectId, enrollmentId),
    foreign key (subjectId) references Secretary.Subject,
    foreign key (teacherId) references Academy.Teacher on delete set null,
    foreign key (enrollmentId) references Academy.Enrollment
);

create table if not exists Academy.Student
(
    studentId varchar(15) not null,
    enrollmentId varchar(15) not null,
    schoolYear varchar(20) not null,
    isApproved boolean,
    isRepeating boolean not null,
    createdAt timestamp with time zone not null, 
    primary key (studentId, enrollmentId),
    foreign key (studentId) references Secretary.Student,
    foreign key (enrollmentId) references Academy.Enrollment
);

create table if not exists Academy.Semester
(
    semesterId serial not null primary key,
    schoolyear varchar(20) not null,
    semester int not null,
    deadLine date not null,
    isActive boolean not null ,
    label varchar(30) not null ,
    foreign key (schoolyear) references Secretary.Schoolyear
);

create table if not exists Academy.Partial
(
    partialId serial not null primary key,
    semesterId int not null,
    partial int not null,
    startDate date not null,
    deadLine date not null,
    isActive boolean not null,
    label varchar(20) not null,
    gradeRecordIsActive boolean not null,
    foreign key (semesterId) references Academy.Semester
);

create table if not exists Academy.Subject_Partial
(
    subjectPartialId serial not null primary key,
    subjectId varchar(15) not null,
    enrollmentId varchar(15) not null,
    partialId smallint not null,
    teacherId varchar(15) not null,
    foreign key (subjectId, enrollmentId) references Academy.Subject,
    foreign key (partialId) references Academy.Partial,
    foreign key (teacherId) references Academy.Teacher
);

create table if not exists Academy.Grade
(
    gradeId serial not null primary key,
    studentId varchar(15) not null,
    subjectPartialId int not null,
    grade float,
    conductGrade float,
    label varchar(10),
    foreign key (subjectPartialId) references Academy.Subject_Partial,
    foreign key (studentId) references secretary.student 
);