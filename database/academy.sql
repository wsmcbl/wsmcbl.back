create schema if not exists Academy;

-- Generate secretary.subject id
CREATE SEQUENCE if not exists academy.enrollment_id_seq START 10;

CREATE OR REPLACE FUNCTION academy.generate_enrollment_id()
    RETURNS varchar(20) AS $$
DECLARE
    seq_part CHAR(5);
BEGIN
    seq_part := LPAD(NEXTVAL('academy.enrollment_id_seq')::TEXT, 5, '0');

    Return 'enr' || seq_part;
END;
$$ LANGUAGE plpgsql;


create table if not exists Academy.Enrollment
(
    enrollmentId varchar(15) primary key default academy.generate_enrollment_id(),
    degreeId varchar(25) not null,
    label varchar(20) not null,
    schoolYear varchar(20) not null,
    section varchar(10) not null,
    capacity smallint,
    quantity smallint,
    foreign key (degreeId) references secretary.Degree,
    foreign key (schoolYear) references Secretary.Schoolyear
);

create table if not exists Academy.Teacher
(
    teacherId varchar(15) primary key,
    userId varchar(15) not null,
    enrollmentId varchar(20),
    isGuide boolean not null,
    foreign key (userId) references Config.User,
    foreign key (enrollmentId) references Academy.Enrollment on delete set null
);

create table if not exists Academy.Subject
(
    subjectId varchar(15) not null,
    enrollmentId varchar(15) not null,
    teacherId varchar(15),
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
    primary key (studentId, enrollmentId),
    isApproved boolean,
    foreign key (studentId) references Secretary.Student,
    foreign key (enrollmentId) references Academy.Enrollment
);

create table if not exists Academy.Grade
(
    scoreId serial not null primary key,
    studentId varchar(15) not null,
    subjectId varchar(15) not null,
    enrollmentId varchar(15) not null,
    partialId int not null,
    grade float,
    foreign key (studentId, enrollmentId) references Academy.Student (studentId, enrollmentId),
    foreign key (subjectId, enrollmentId) references Academy.Subject (subjectid, enrollmentId),
    foreign key (partialId) references Academy.Partial (partialId)
);

create table if not exists Academy.Semester
(
    semesterId serial not null primary key,
    schoolyear varchar(20) not null,
    semester int not null,
    deadLine date,
    isActive boolean,
    label varchar(20),
    foreign key (schoolyear) references Secretary.Schoolyear
);

create table if not exists Academy.Partial
(
    partialId serial not null primary key,
    semesterId int not null,
    partial int not null,
    deadLine date,
    foreign key (semesterId) references Academy.Semester
);