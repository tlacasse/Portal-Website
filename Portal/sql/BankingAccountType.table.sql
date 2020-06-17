CREATE TABLE BankingAccountType (
    Id          INTEGER      PRIMARY KEY ON CONFLICT FAIL AUTOINCREMENT
                             NOT NULL ON CONFLICT FAIL,
    Name        VARCHAR (30) UNIQUE ON CONFLICT FAIL
                             NOT NULL ON CONFLICT FAIL,
    DateUpdated DATETIME     NOT NULL ON CONFLICT FAIL
);
