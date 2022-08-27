CREATE DATABASE zdatabase;
USE zdatabase;

CREATE TABLE users(
	id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_users PRIMARY KEY,
    [name] VARCHAR(100),
    email VARCHAR(200) NOT NULL,
    [password] VARCHAR(1000) NOT NULL,
    [active] BIT NOT NULL,
    [approvedDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE documents
(
	[id] INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_documents PRIMARY KEY,
    [idUser] INT NOT NULL,
	[idDocumentType] INT,
    [number] VARCHAR(100) NOT NULL,
	[issueDate] DATETIME NOT NULL,
    CONSTRAINT FK_user_documents FOREIGN KEY (idUser) REFERENCES users(id)
);

CREATE TABLE nullTests(
	id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_nullTests PRIMARY KEY,
    [name] VARCHAR(100),
    [phoneNumber] INT,
    [expirationDate] DATETIME,
    [inactive] BIT
);

CREATE TABLE [oldUsers] (
  [id] INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_oldUsers PRIMARY KEY,
  [strName] VARCHAR(100) NULL,
  [strEmail] VARCHAR(200) NOT NULL,
  [strPassword] VARCHAR(1000) NOT NULL,
  [blnActive] BIT NOT NULL,
  [approvedDt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
);

GO

SELECT * FROM users;
INSERT INTO users ([name], [email], [password], [active], [approvedDate])
VALUES
('I''m the new user', 'gabriel.s479@hotmail.com','NotThatSecret', 0, '2022-06-13 15:06:00'),
('I''m the new user3', 'gabriel.s479@hotmail.com', 'NotThatSecret', 1, '2022-06-14 18:37:55'),
('I''m the new user3', 'gabriel.s479@hotmail.com', 'NotThatSecret', 1, '2022-06-14 21:12:57'),
('Transaction', 'gabriel.s479@hotmail.com', 'NotThatSecret', 0, '2022-06-14 16:30:01'),
('Transaction', 'gabriel.s479@hotmail.com', 'NotThatSecret', 0, '2022-06-19 18:36:48'),
('Transaction',	'gabriel.s479@hotmail.com',	'NotThatSecret', 0, '2022-06-23 14:20:05');

SELECT * FROM documents;
INSERT INTO documents([idUser], [idDocumentType], [number], [issueDate])
VALUES
('1', '12', '123321', '2022-06-24 00:00:00');

SELECT * FROM nullTests;
INSERT INTO nullTests([name], [phoneNumber], [expirationDate], [inactive])
VALUES
(null, null, null, null),
(null, null, null, null),
(null, null, null, null),
(null, null, null, null),
(null, null, null, null),
('VALUE HERE', null, null, null);

SELECT * FROM oldUsers;
INSERT INTO oldUsers([strName], [strEmail], [strPassword], [blnActive], [approvedDt])
VALUES ('gabriel', 'sales.g479@hotmail.com', 'SuperSecret', 1, '2019-06-10');

ALTER TABLE users
ADD [height] DECIMAL(8, 4);

GO

UPDATE users SET [height] = '1.75' WHERE ID < 5;
