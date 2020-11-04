use employee_payroll;

alter procedure spUpdatingSalaryFromMultipleTable
(
@salary int,
@id int,
@name varchar(50)
)
as
begin
update payroll
set payroll.basepay=@salary
from payroll 
join employee
on employee.salaryid= payroll.salary_id
where employee.id= @id and employee.name= @name
end;

select * from payroll;
select * from employee;
Select * from employee join payroll on payroll.salary_id=employee.salaryid where id=32