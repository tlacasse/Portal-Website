CREATE TABLE PortalIconHistory (
    Id          INTEGER       PRIMARY KEY AUTOINCREMENT,
    IconId      INTEGER       REFERENCES PortalIcon (Id),
    Name        VARCHAR (30),
    Image       VARCHAR (10),
    Link        VARCHAR (500),
    IsNew       BOOLEAN,
    DateUpdated DATETIME
);
