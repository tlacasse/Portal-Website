CREATE TABLE BankingSubcategory (
    Id          INTEGER       PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                              NOT NULL ON CONFLICT FAIL,
    Name        VARCHAR (100) UNIQUE ON CONFLICT FAIL
                              NOT NULL ON CONFLICT FAIL,
    CategoryId  INTEGER       REFERENCES BankingCategory (Id) 
                              NOT NULL ON CONFLICT FAIL,
    DateUpdated DATETIME      NOT NULL ON CONFLICT FAIL
);
