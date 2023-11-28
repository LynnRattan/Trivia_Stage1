CREATE DATABASE["Trivia"];

use ["Trivia"]

CREATE TABLE["Players"]
(
[playerMail] nvarchar(250) PRIMARY KEY not null,
[name] nvarchar(250) not null,
[levelCode] int  not null FOREIGN KEY References Levels(levelCode),
[points] int not null
);

CREATE TABLE ["Levels"]
(
[levelCode] int Identity(1,1) PRIMARY KEY not null,
[name] nvarchar(250) not null
);

CREATE TABLE["Questions"]
(
[questionId] int PRIMARY KEY not null,
[subjectCode] int not null,
);
