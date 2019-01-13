CREATE VIEW vwPortalGrid AS
    SELECT PG.Id AS GridId,
           PG.XCoord,
           PG.YCoord,
           PG.DateUsed,
           PI.Id,
           PI.Name,
           PI.Image,
           PI.Link,
           PI.DateCreated,
           PI.DateChanged
      FROM PortalGrid AS PG
           INNER JOIN
           PortalIcon AS PI ON PG.Icon = PI.Id
     WHERE Active = 1;
