﻿CREATE TABLE LogRecord (
    Id        INTEGER        PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT,
    Date      DATETIME,
    Context   VARCHAR (255),
    Message   VARCHAR (8000),
    Exception VARCHAR (255) 
);
