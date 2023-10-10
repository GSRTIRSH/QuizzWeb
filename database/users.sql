CREATE TABLE users (
    id SERIAL PRIMARY KEY, 
    first_name VARCHAR(255) NOT NULL,
    sub_name VARCHAR(255) NULL,
    email VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    token VARCHAR(255) NULL
    my-quizzes INTEGER[] NULL
);