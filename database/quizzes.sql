CREATE TYPE DIFFICULTY AS ENUM ('Easy', 'Medium', 'Hard');

CREATE TABLE quizzes(
    id SERIAL PRIMARY KEY,
    question TEXT NOT NULL,
    description TEXT NULL, 
    answers JSONB NOT NULL, 
    multiple_correct_answers BOOLEAN NOT NULL,
    correct_answers JSONB NOT NULL, 
    correct_answer TEXT NULL,
    explanation TEXT NULL, 
    tip TEXT NULL,
    tags JSONB NOT NULL, 
    category TEXT NOT NULL,
    difficulty TEXT NOT NULL,
    author INTEGER NULL
);