CREATE TABLE PortalIcon (
    Id          INTEGER       PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,
    Name        VARCHAR (30)  UNIQUE ON CONFLICT FAIL,
    Image       VARCHAR (10),
    Link        VARCHAR (500),
    DateCreated DATETIME,
    DateChanged DATETIME
);
