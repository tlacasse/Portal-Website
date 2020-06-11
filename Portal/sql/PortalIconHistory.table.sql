CREATE TABLE PortalIconHistory (
    Id          INTEGER       PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                              NOT NULL ON CONFLICT FAIL,
    IconId      INTEGER       REFERENCES PortalIcon (Id) 
                              NOT NULL ON CONFLICT FAIL,
    Name        VARCHAR (30)  NOT NULL ON CONFLICT FAIL,
    Image       VARCHAR (10)  NOT NULL ON CONFLICT FAIL,
    Link        VARCHAR (500) NOT NULL ON CONFLICT FAIL,
    IsNew       BOOLEAN       NOT NULL ON CONFLICT FAIL,
    DateUpdated DATETIME      NOT NULL ON CONFLICT FAIL
);
