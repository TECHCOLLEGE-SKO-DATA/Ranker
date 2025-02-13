--DROP TABLE IF EXISTS scoreBoard;
CREATE TABLE scoreBoard (
    scoreBoardId INTEGER PRIMARY KEY AUTOINCREMENT, 
    name TEXT NOT NULL, 
    description TEXT NOT NULL, 
    uniqueKey TEXT NOT NULL UNIQUE, 
    settingId INTEGER NOT NULL
);


--DROP TABLE IF EXISTS score;
CREATE TABLE score (
    scoreId INTEGER PRIMARY KEY AUTOINCREMENT, 
    participantName TEXT NOT NULL, 
    points INTEGER NOT NULL, 
    timer REAL NOT NULL,
    scoreBoardId INTEGER,
    FOREIGN KEY (scoreBoardId) REFERENCES scoreBoard(scoreBoardId)
);
