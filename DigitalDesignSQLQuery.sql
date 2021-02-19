use digitalDesignEmployee;
go

--1.	���������� � ������������ ���������� ������.
select
id,
[name],
salary
from Employee
where salary = (select max(salary) from Employee)
go


--2.	�����, � ������� �������� ��������� � ����� ������� ���������.
select
Employee.department_id,
Department.[name] as department
from Employee
inner join Department on Department.id = Employee.department_id
where Employee.salary = (select max(Employee.salary) from Employee)
go


--3.	�����, � ������������ ��������� ��������� �����������.
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


--4.	����������, ��� ��� ���������� �� �л � ������������� �� ��.
select id, [name] from Employee
where [name] like '�%�'