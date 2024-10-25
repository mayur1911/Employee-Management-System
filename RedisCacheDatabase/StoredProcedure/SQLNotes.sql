-- select 2nd Highest salary
SELECT MAX(Salary) AS SecondHighestSalary
FROM Manager
WHERE Salary < (SELECT MAX(Salary) FROM Manager);

-- Inner join to fetch common records
select e.*,m.* from Employee e 
INNER JOIN Manager m
on m.ManagerID = e.ManagerId

-- avoid duplicate rows
-- this will get all diff managerNames
select distinct managername from Manager

-- using group by with having
-- this will group by the records on the basis of managername
-- then filter the name count  = 1 using having 
select  managername, count(*)  from Manager
group by ManagerName
having count(*) = 1


-- get salry greater than avg salary
select m.ManagerName, m.salary,* from Manager m
where m.salary > (select avg(salary) from Manager)

-- Write a query to find all employees with duplicate salaries in the Manager table.
SELECT Salary, COUNT(*)
FROM Manager
GROUP BY salary
HAVING COUNT(*) > 1;

-- Write a query to find the top N salaries in the Employee table.
SELECT DISTINCT Salary
FROM Manager
ORDER BY Salary DESC
OFFSET 0 ROWS FETCH NEXT N ROWS ONLY;  -- Replace N with the desired number (e.g., 3 for the top 3 salaries)

-- find salary in between
select salary,* from manager where salary  between 2300 and 3400
