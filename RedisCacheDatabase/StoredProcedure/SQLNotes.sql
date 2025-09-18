select UPPER(ManagerName),* from Manager
SELECT DISTINCT(Salary) FROM Manager


--- CREATE copy of one table 
SELECT * INTO DUMMY FROM Manager
SELECT * FROM DUMMY

-- replace char in value
SELECT REPLACE(ManagerName,'M','T') FROM Manager


-- concat two columns
select concat(ManagerName ,' -> ',ManagerDesignation) from Manager

-- get manager with salary eithr with 20k or 30k
select * from Manager where Salary in (20000,30000)


-- Get 2nd highest salary using TOP
select top 1 salary from 
( select top 2 salary from manager order by Salary desc) as t order by Salary asc
-- using nested statement above

--Now second actual max salary
select top 1 salary from 
( select distinct top 2  salary from manager order by Salary desc) as t order by Salary asc

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
