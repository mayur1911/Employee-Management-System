CREATE PROCEDURE sp_GetManagerById
 @ManagerId AS INT
AS
BEGIN
    SELECT ManagerID, ManagerName, ManagerDesignation, ProjectName, Salary
    FROM Manager WITH (NOLOCK)
    WHERE ManagerID = @ManagerID;
END
GO