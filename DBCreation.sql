CREATE TABLE Levels (
    LevelID VARCHAR(20) PRIMARY KEY,
    DisplayName VARCHAR(100)
);
CREATE TABLE Players (
    PlayerID INT PRIMARY KEY,
    Username VARCHAR(100)
);
CREATE TABLE PlayerTimes (
    EntryID INT IDENTITY(1,1) PRIMARY KEY,
    LevelID VARCHAR(20),
    PlayerID INT,
    CompletionTime FLOAT,
    FOREIGN KEY (LevelID) REFERENCES Levels(LevelID),
    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
);
