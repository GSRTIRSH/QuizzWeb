DROP TABLE IF EXISTS UserData;
CREATE TABLE UserData(ID integer PRIMARY KEY, description text, quizzes integer);

INSERT INTO UserData(ID, description, quizzes) VALUES 
    (0, 'first', 12),
    (1, 'sdfffd', 31),
    (2, 'qqpood', 99);

DROP TABLE IF EXISTS UserCreds;
CREATE TABLE UserCreds(ID integer, nickname char(20) UNIQUE, email char(20) UNIQUE, password char(30), token uuid);

INSERT INTO UserCreds(ID, nickname, email, password, token) VALUES 
    (0, 'nick', 'nick@gmail.com', 'AAff99eeAdf', gen_random_uuid()),
    (1, 'alex', 'alex@gmail.com', 'FAAff99eeAdf', gen_random_uuid()),
    (2, 'bob', 'bob@gmail.com', 'CAAff99eeAdf', gen_random_uuid());

SELECT * FROM UserData, UserCreds WHERE (UserData.Id = UserCreds.Id);
