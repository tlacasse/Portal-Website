CREATE VIEW vwPortalIcon AS
    SELECT Id,
           Name,
           Image,
           Link,
           DateCreated,
           DateChanged
      FROM PortalIcon;
