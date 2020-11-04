use employee_payroll;

exec insertingdata 30000,3000,27000,2000,25000,15,31,priyanka,'2020-11-04','F',3334343434,'Barwala',106,10,'HR',50,'Vishal','Infosys';


alter Procedure insertingdata
(
@basepay decimal,
@deductions decimal,
@taxable_pay decimal,
@tax decimal,
@netpay decimal,
@salaryid int,
@employeeid int,
@name varchar(150),
@start date,
@gender varchar(2),
@phone_number bigint,
@address varchar(150),
@company_id int,
@departmentid int,
@departmentname varchar(50),
@noOfEmployees int,
@headofDepartment varchar(50),
@companyname varchar(50)
)
as
Begin
     --set nocount on added to prevent extra result sets from
	 --interfering with select statements
	 Set nocount on;
	 Begin transaction
		begin try
			--insert into company table
			insert into company values(@company_id,@companyname);
			--insert into department table
			insert into departments values(@departmentid,@departmentname,@noOfEmployees,@headofDepartment);
					--insert into payroll table secondly
			insert into payroll values(@basepay,@deductions,@taxable_pay,@tax,@netpay,@salaryid);
		    --insert into employeetable first
			insert into employee values(@employeeid,@name,@start,@gender,@phone_number,@address,@company_id, @salaryid);
			--insert into employee department table
			insert into EmployeeDepartment values(@employeeid,@departmentid);
		
		
		--if not error, commit transaction 
		commit transaction
		End Try
		Begin catch
		   --if error, roll back changes done by any of the sql queries
		  Rollback transaction
		End catch
End




