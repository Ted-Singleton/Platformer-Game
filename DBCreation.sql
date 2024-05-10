--TABLE CREATION
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

--INITIAL DATA INSERTION
INSERT INTO Levels(LevelID, DisplayName) VALUES
('LevelTest', 'Test Level'),
('Level1-1', 'Ch1Lv1'),
('Level1-2', 'Ch1Lv2'),
('Level1-3', 'Ch1Lv3');

INSERT INTO Players(PlayerID, Username) VALUES
(0, 'TestPlayer'),
(1, 'RealPlayer');

INSERT INTO PlayerTimes(PlayerID, LevelID, CompletionTime) VALUES
(0, 'Level1-1', 10.32),
(0, 'Level1-2', 9.74),
(0, 'Level1-3', 1.32);
