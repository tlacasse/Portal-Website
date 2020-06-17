CREATE TABLE BankingAccount (
    Id            INTEGER       PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                                NOT NULL ON CONFLICT FAIL,
    Name          VARCHAR (100) UNIQUE ON CONFLICT FAIL
                                NOT NULL ON CONFLICT FAIL,
    AccountTypeId INTEGER       REFERENCES BankingAccountType (Id) 
                                NOT NULL ON CONFLICT FAIL,
    DateUpdated   DATETIME      NOT NULL ON CONFLICT FAIL
);
