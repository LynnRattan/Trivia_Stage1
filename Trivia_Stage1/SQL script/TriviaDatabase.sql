CREATE DATABASE [Trivia]

use [Trivia]

CREATE TABLE [Subjects]
(
[subjectCode] int Identity(1,1) PRIMARY KEY not null,
[name] nvarchar(250) not null
);

CREATE TABLE [Status]
(
[statusCode] int Identity(1,1) PRIMARY KEY not null,
[name] nvarchar(250)  not null
);

CREATE TABLE [Levels]
(
[levelCode] int Identity(1,1) PRIMARY KEY not null,
[name] nvarchar(250) not null
);

CREATE TABLE[Players]
(
[playerMail] nvarchar(250) PRIMARY KEY not null,
[name] nvarchar(250) not null,
[levelCode] int  not null FOREIGN KEY References Levels(levelCode),
[points] int not null
);


CREATE TABLE[Questions]
(
[questionId] int PRIMARY KEY not null,
[subjectCode] int not null FOREIGN KEY References Subjects(subjectCode),
[text] nvarchar(250) not null,
[correctAns] nvarchar(250) not null,
[wrongAns1] nvarchar(250) not null,
[wrongAns2] nvarchar(250) not null,
[wrongAns3] nvarchar(250) not null,
[createdBy] nvarchar(250)  not null FOREIGN KEY References Players(playerMail),
[statusCode] int not null FOREIGN KEY References Status(statusCode)
);

INSERT INTO [Levels] (name) values('rookie');
INSERT INTO [Levels] (name) values('master');
INSERT INTO [Levels] (name) values('manager');

INSERT INTO [Players] (playerMail, name,levelCode, points ) values('ronen@gmail.com', 'Ronen',3,1000);

INSERT INTO [Subjects] (name) values('sports');
INSERT INTO [Subjects] (name) values('politics');
INSERT INTO [Subjects] (name) values('history');
INSERT INTO [Subjects] (name) values('science');
INSERT INTO [Subjects] (name) values('ramonHighSchool');

INSERT INTO [Status] (name) values('approved');
INSERT INTO [Status] (name) values('notApproved');
INSERT INTO [Status] (name) values('waiting');

INSERT INTO [Questions] (questionId, subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(1, 1, 'How many km are in a marathon?','42.4','3.2','100','1', 'ronen@gmail.com',1);

INSERT INTO [Questions] (questionId, subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(2,2,'Who is the USA president?', 'Joe Baiden', 'Barak Obama','Donald Trump','Bibi', 'ronen@gmail.com',1);

INSERT INTO [Questions] (questionId, subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(3,3,'When did WWI started?','1914','1932','1917','1915','ronen@gmail.com',1);

INSERT INTO [Questions] (questionId, subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(4,4, 'What is the atomic number of oxygen?','8','2','7','12','ronen@gmail.com',1);

INSERT INTO [Questions] (questionId, subjectCode, text, correctAns, wrongAns1, wrongAns2, wrongAns3, createdBy, statusCode) values(5,5, 'What is the name og the principle in Ramon High School?', 'Hana', 'Leah', 'Rebecca','Moshe','ronen@gmail.com',1);

