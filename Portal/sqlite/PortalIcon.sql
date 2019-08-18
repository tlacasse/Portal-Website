CREATE TABLE PortalIcon (
    Id          INTEGER       PRIMARY KEY ON CONFLICT ABORT AUTOINCREMENT,
    Name        VARCHAR (30)  UNIQUE ON CONFLICT ABORT
                              NOT NULL,
    Image       VARCHAR (10),
    Link        VARCHAR (500),
    DateCreated DATETIME,
    DateChanged DATETIME
);
