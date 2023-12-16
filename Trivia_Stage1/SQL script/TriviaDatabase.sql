CREATE DATABASE [Trivia]

use [Trivia]

CREATE TABLE [Subjects]
(
[subjectCode] int IDENTITY (1, 1) PRIMARY KEY,
[name] nvarchar(250) not null
);

CREATE TABLE [Status]
(
[statusCode] int IDENTITY (1, 1) PRIMARY KEY,
[name] nvarchar(250)  not null
);

CREATE TABLE [Levels]
(
[levelCode] int IDENTITY (1, 1) PRIMARY KEY,
[name] nvarchar(250) not null
);

CREATE TABLE[Players]
(
[playerId] int PRIMARY KEY IDENTITY (1, 1),
[playerMail] nvarchar(250) not null,
[name] nvarchar(250) not null,
[password] nvarchar(250) not null,
[levelCode] int  not null FOREIGN KEY References Levels(levelCode),
[points] int not null
);


CREATE TABLE[Questions]
(
[questionId] int PRIMARY KEY IDENTITY (1, 1),
[subjectCode] int not null FOREIGN KEY References Subjects(subjectCode),
[text] nvarchar(250) not null,
[correctAns] nvarchar(250) not null,
[wrongAns1] nvarchar(250) not null,
[wrongAns2] nvarchar(250) not null,
[wrongAns3] nvarchar(250) not null,
[createdBy] int  not null FOREIGN KEY References Players(playerId),
[statusCode] int not null FOREIGN KEY References Status(statusCode)
);

INSERT INTO [Levels] (name) values('rookie');
INSERT INTO [Levels] (name) values('master');
INSERT INTO [Levels] (name) values('manager');

INSERT INTO [Players] (playerMail, [name], [password], levelCode, points ) values('ronen@gmail.com', 'Ronen','12345',3,100);

INSERT INTO [Subjects] (name) values('sports');
INSERT INTO [Subjects] (name) values('politics');
INSERT INTO [Subjects] (name) values('history');
INSERT INTO [Subjects] (name) values('science');
INSERT INTO [Subjects] (name) values('ramonHighSchool');

INSERT INTO [Status] (name) values('approved');
INSERT INTO [Status] (name) values('notApproved');
INSERT INTO [Status] (name) values('waiting');

INSERT INTO [Questions] (subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(1, 'How many km are in a marathon?','42.4','3.2','100','1', 1,1);

INSERT INTO [Questions] (subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(2,'Who is the USA president?', 'Joe Baiden', 'Barak Obama','Donald Trump','Bibi', 1,1);

INSERT INTO [Questions] (subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(3,'When did WWI started?','1914','1932','1917','1915',1,1);

INSERT INTO [Questions] (subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(4, 'What is the atomic number of oxygen?','8','2','7','12',1,1);

INSERT INTO [Questions] (subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(5, 'What is the name og the principle in Ramon High School?', 'Hana', 'Leah', 'Rebecca','Moshe',1,1);



