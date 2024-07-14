create schema if not exists Academy;

create table if not exists Academy.Enrollment
(
    enrollmentId varchar(15) primary key,
    enrollmentLabel varchar(20) not null,
    schoolYear varchar(20) not null,
    section varchar(2) not null,
    capacity smallint,
    quantity smallint,
    gradeId int not null,
    foreign key (gradeId) references secretary.Grade,
    foreign key (schoolYear) references Secretary.Schoolyear
);

create table if not exists Academy.Teacher
(
    teacherId varchar(15) primary key,
    userId varchar(15) not null,
    enrollmentId varchar(20),
    foreign key (userId) references Config.User,
    foreign key (enrollmentId) references Academy.Enrollment
);

create table if not exists Academy.Subject
(
    subjectId varchar(15) primary key,
    baseSubjectId varchar(15) not null,
    teacherId varchar(15) not null,
    enrollmentId varchar(15) not null,
    foreign key (baseSubjectId) references Secretary.Subject,
    foreign key (teacherId) references Academy.Teacher,
    foreign key (enrollmentId) references Academy.Enrollment
);

create table if not exists Academy.Student
(
    studentId varchar(15) not null,
    enrollmentId varchar(15) not null,
    schoolYear varchar(20) not null,
    primary key (studentId, enrollmentId),
    isApproved boolean,
    foreign key (studentId) references Secretary.Student,
    foreign key (enrollmentId) references Academy.Enrollment
);

create table if not exists Academy.Note
(
    studentId varchar(15) not null,
    subjectId varchar(15) not null,
    enrollmentId varchar(15) not null,
    cumulative float,
    exam float,
    finalScore float,
    primary key (studentId, subjectId),
    foreign key (studentId, enrollmentId) references Academy.Student (studentId, enrollmentId),
    foreign key (subjectId) references Academy.Subject
);