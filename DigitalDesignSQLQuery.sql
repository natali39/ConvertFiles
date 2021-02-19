use digitalDesignEmployee;
go

--1.	Сотрудника с максимальной заработной платой.
select
id,
[name],
salary
from Employee
where salary = (select max(salary) from Employee)
go


--2.	Отдел, в котором работает сотрудник с самой высокой зарплатой.
select
Employee.department_id,
Department.[name] as department
from Employee
inner join Department on Department.id = Employee.department_id
where Employee.salary = (select max(Employee.salary) from Employee)
go


--3.	Отдел, с максимальной суммарной зарплатой сотрудников.
with t as
(
select Department.[name] as department, sum(Employee.salary) as total_salary
from Employee
inner join Department on Department.id = Employee.department_id
group by Department.[name]
)
select department, total_salary
from t
where total_salary = (select max(total_salary) from t)
go


--4.	Сотрудника, чье имя начинается на «Р» и заканчивается на «н».
select id, [name] from Employee
where [name] like 'Р%н'