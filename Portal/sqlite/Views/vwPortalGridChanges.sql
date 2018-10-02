CREATE VIEW vwPortalGridChanges AS
    SELECT G.DateUsed AS DateTime,
           '''' || I.Name || ''' ' || CASE WHEN Active = 1 THEN 'placed' ELSE 'removed' END || ' at (' || XCoord || ',' || YCoord || ')' AS Event
      FROM PortalGrid AS G
           INNER JOIN
           PortalIcon AS I ON G.Icon = I.Id
    UNION ALL
    SELECT DateUpdated AS DateTime,
           '''' || Name || ''' (' || SUBSTR([REPLACE]([REPLACE](Link, 'http://', ''), 'https://', ''), 1, 20) || '...) ' || CASE WHEN IsNew = 1 THEN 'added' ELSE 'updated' END AS Event
      FROM PortalIconHistory
     ORDER BY DateTime DESC;
