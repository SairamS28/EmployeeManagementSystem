create database EmployeeManagementSystem
use EmployeeManagementSystem
create table Department(
Departmentid tinyint primary key,
[Name] varchar(25) not null
)
create table Employee(
Empid int primary key,
[Name] varchar(50) not null,
[Password] varchar(30) not null,
Departmentid tinyint foreign key references Department(Departmentid),
[Address] varchar(50),
Mobile bigint not null,
Email varchar(50) not null,
Managerid int foreign key references Employee(Empid)
)
create table [admin](
Userid int primary key,
[Password] varchar(30) not null
)
