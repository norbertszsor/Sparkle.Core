CREATE TABLE IF NOT EXISTS Companies (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Meters (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    CompanyId TEXT,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL,
    FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);

CREATE TABLE IF NOT EXISTS Readings (
    Id TEXT PRIMARY KEY,
    Value REAL NOT NULL,
    MeterId TEXT,
    CreatedAt TEXT NOT NULL,
    UpdatedAt TEXT NOT NULL,
    FOREIGN KEY (MeterId) REFERENCES Meters(Id)
);