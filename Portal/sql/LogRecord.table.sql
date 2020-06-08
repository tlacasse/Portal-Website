CREATE TABLE LogRecord (
    Id        INTEGER        PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                             NOT NULL ON CONFLICT FAIL,
    Date      DATETIME       NOT NULL ON CONFLICT FAIL,
    Context   VARCHAR (255),
    Message   VARCHAR (8000),
    Exception VARCHAR (255) 
);
