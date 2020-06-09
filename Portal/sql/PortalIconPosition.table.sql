﻿CREATE TABLE PortalIconPosition (
    Id          INTEGER  PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                         NOT NULL ON CONFLICT FAIL,
    IconId      INTEGER  REFERENCES PortalIcon (Id) 
                         NOT NULL ON CONFLICT FAIL,
    XCoord      INTEGER  NOT NULL ON CONFLICT FAIL,
    YCoord      INTEGER  NOT NULL ON CONFLICT FAIL,
    IsActive    BOOLEAN  NOT NULL ON CONFLICT FAIL,
    DateUpdated DATETIME
);
