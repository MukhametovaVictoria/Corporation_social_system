CREATE DATABASE Employees

USE Employees;
CREATE TABLE Employee (
	Id uniqueidentifier NOT NULL,
	Firstname nvarchar(max) NOT NULL,
	Surname nvarchar(max) NULL,
	Position nvarchar(max) NULL,
	IsDeleted bit NOT NULL,
	IsAdmin bit NOT NULL,
	Birthdate datetime2(7) NOT NULL,
	EmploymentDate datetime2(7) NOT NULL,
	MainEmail nvarchar(max) NULL,
	MainTelephoneNumber nvarchar(max) NULL,
	About nvarchar(max) NULL,
	OfficeAddress nvarchar(max) NULL
)
ALTER TABLE Employee ADD  CONSTRAINT [DFHuuuvW8Ndkv0vWuFOKvZq9Fs]  DEFAULT (newid()) FOR Id
ALTER TABLE Employee ADD  DEFAULT (0) FOR IsDeleted
ALTER TABLE Employee ADD  DEFAULT (0) FOR IsAdmin
