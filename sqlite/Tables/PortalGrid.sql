CREATE TABLE PortalGrid (
    Id       INTEGER  PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,
    Icon     INTEGER  REFERENCES PortalIcon (Id),
    XCoord   INTEGER,
    YCoord   INTEGER,
    DateUsed DATETIME,
    Active   BOOLEAN  DEFAULT (1) 
);
