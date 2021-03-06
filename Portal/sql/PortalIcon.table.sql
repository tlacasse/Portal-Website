﻿CREATE TABLE PortalIcon (
    Id          INTEGER       PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                              NOT NULL ON CONFLICT FAIL,
    Name        VARCHAR (30)  UNIQUE ON CONFLICT FAIL
                              NOT NULL ON CONFLICT FAIL,
    Image       VARCHAR (10)  NOT NULL ON CONFLICT FAIL,
    Link        VARCHAR (500) NOT NULL ON CONFLICT FAIL,
    DateCreated DATETIME,
    DateChanged DATETIME
);
