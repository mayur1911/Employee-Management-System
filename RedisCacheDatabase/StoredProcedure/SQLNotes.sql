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

-- Get salary between range 20k to 30k
select * from Manager where Salary between 20000 and 30000

-- managerName starts with m
select * from Manager  where ManagerName like 'm%'
-- starts with m and end with 1
select * from Manager  where ManagerName like 'm%1'
-- managerName that contains h char
select * from Manager  where ManagerName like '%h%'
-- managerName end with 'n' and have 5 letters total
select * from Manager  where ManagerName like '____%n'

--Write a SQL query to print details of the Workers who have Q joined in Mar’2019.
select * from Manager where month(joiningDate) = 03 and year(joiningDate) = 2019
-- count of manager withe date after '2019/02/02'
select count(*) from Manager where joiningdate > '2019/02/02'

-- get projectname and their count in ascending order
-- Count returns single int value so we need group By here for aggregate functions
select ProjectName, count(ProjectName) as ProjectCount
from Manager  group by ProjectName order by ProjectName asc


-- get salary given for each project 
select  ProjectName,sum(Salary) as ProjectTotalSalary
from Manager group by  ProjectName

-- get project names with managerCount > 2
select  ProjectName ,count(ManagerName) as ProjectTotalSalary
from Manager group by  ProjectName  having count(ManagerName) > 2

-- get current date and time
SELECT getdate();

-- filter by Date
select count(*) from Manager where joiningdate > '2019/02/02'

-- UNION ALL keeps duplicate records
-- UNION removes duplicate record

-- Manger name with highest salary
select top 1 ManagerName from Manager  order by Salary desc
-- below query will return multiple records if more than 1 manager are there with max salary

select ManagerName,Salary from Manager where Salary = (select max(salary) from manager)


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

-- select nth highest salary if some of salary are same
select top 1 t.salary from 
(select top n salary from Manager order by Salary desc) as t order by Salary asc

-- select nth highest salary without using TOP
select salary from Manager m1 where n-1 = (select count(distinct(salary)) from Manager m2 
where m1.Salary <= m2.Salary)

-- select nth highest salary (distinct)
SELECT TOP 1 Salary
FROM (
SELECT DISTINCT TOP 3 Salary
FROM Manager
ORDER BY Salary DESC
)as t
ORDER BY Salary ASC;

-- use dense_rank to get the all 3 highest salary..
-- this assignes rank to each salary as 1, 2, 3
-- same rank to same salary
select salary , ranku from
(
	select salary,
	dense_rank()  over (order by salary desc) as ranku
	from Manager
)
t  where ranku <= 3

-- difference between rank and dense_rank() 
-- Question no 32

-- Get the employee count in each project and fetch only >= 5 emp count project name wise
select count(ProjectName) as 'ProjectEmpCount', ProjectName
from Manager  group by ProjectName  having count(projectname) >= 5

-- get employee details with its manager using inner join
select e.*,m.* from Employee e
inner  join Manager m on m.ManagerID = e.ManagerId

-- get manager names with minimum salary
select ManagerName from Manager where Salary = 
(select min(Salary) from Manager)

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
