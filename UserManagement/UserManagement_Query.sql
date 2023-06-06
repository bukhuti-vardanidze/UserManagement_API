CREATE DATABASE UserManagement;


USE UserManagement;

CREATE TABLE Users
(
Id int IDENTITY(1,1) Primary Key,
UserName varchar(255) NOT NULL,
Email varchar(255) NOT NULL,
Password varchar(255) NOT NULL, 
IsActive int NOT NULL
);

CREATE TABLE UserProfiles
(
Id int IDENTITY(1,1) PRIMARY KEY,
FirstName varchar(255) NOT NULL,
LastName varchar(255) NOT NULL,
PersonalNumber varchar(11) NOT NULL,
UserId int FOREIGN KEY REFERENCES Users(Id)
);


-- EXAMPLE HOW TO ADD INFO IN TABLE

INSERT INTO Users VALUES 
(
'BUTTA',
'bukhuti@gmail.com',
'BUKHUTI123!',
1
)

INSERT INTO UserProfiles VALUES
(
'Bukhuti',
'Vardanidze',
'12345678910',
1
)

SELECT * FROM UserProfiles

SELECT * FROM Users

