CREATE PROCEDURE [dbo].[getNthHighestSalary]
	@salaryRank int = 0
AS
BEGIN
	WITH SalaryRank AS (
    SELECT Salary,
            DENSE_RANK() OVER (ORDER BY Salary DESC) AS SalaryRank
    FROM Manager
    )
    SELECT Salary
    FROM SalaryRank
    WHERE SalaryRank = @salaryRank;  -- Replace N with the desired rank (e.g., 3 for the 3rd highest)
END
