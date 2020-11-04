using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.NETDemo
{
    class EmployeeRepositoryCaller
    {
        public static void GettingAllData()
        {
            //Instatiating employee repository class that contain methods for database connection and retrieval
            EmployeeRepository employeeRepository = new EmployeeRepository();
            //defining list to print all the details from the database
            List<EmployeeModel> list = employeeRepository.GetAllemployee();
            try
            {
                foreach (EmployeeModel employeeModel in list)
                {
                    Console.WriteLine($"Id: {employeeModel.EmployeeID} Name:{employeeModel.EmployeeName} CompanyName: {employeeModel.companyName} DepartmentName: {employeeModel.Department} phoneNumber: {employeeModel.PhoneNumber} gender: {employeeModel.Gender}  address: {employeeModel.Address} netpay={employeeModel.NetPay}");
                }
            }
            //catches exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
        public static void AddingDataInDataBase()
        {
            //instatiating employeemodel object
            EmployeeModel employeeModel = new EmployeeModel();
            //employeeModel.EmployeeID = 04;
            employeeModel.EmployeeName = "Akshay";
            employeeModel.Gender = "M";
            employeeModel.PhoneNumber = 8585858585;
            employeeModel.StartDate = Convert.ToDateTime("2020-09-18");
            //employeeModel.BasicPay = 500000;
            //employeeModel.Deductions = 50000;
            //employeeModel.TaxablePay = 450000;
            //employeeModel.Tax = 50000;
            //employeeModel.NetPay = 400000;
            employeeModel.Address = "Bangalore";
            employeeModel.companyId = 101;
            employeeModel.salaryid = 03;
            //employeeModel.companyName = "Capgemini India";
            //instatiating employee repository
            EmployeeRepository employeeRepository = new EmployeeRepository();
            //passing employee model into method of employee repository class
            bool result = employeeRepository.AddEmployee(employeeModel);
            //printing message on the basis of bool result using ternary condition
            Console.WriteLine(result == true ? "data writtern in database" : "data is not written in database");

        }
        /// <summary>
        /// Updatings the salary.
        /// </summary>
        public static void UpdatingSalary()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            //adding salary and employee id and name whose salary is to be updated
            employeeModel.EmployeeID = 32;
            employeeModel.EmployeeName = "Akshay Poriya";
            employeeModel.BasicPay = 700000;
            EmployeeRepository employeeRepository = new EmployeeRepository();
            bool result = employeeRepository.UpdatingSalaryInDataBase(employeeModel);
            Console.WriteLine(result == true ? "data writtern in database" : "data is not written in database");
        }
        /// <summary>
        /// Readings the updated data.
        /// </summary>
        public static void ReadingUpdatedData()
        {
            EmployeeRepository repository = new EmployeeRepository();
            EmployeeModel employeeModel = new EmployeeModel();
            //defining employee id whose salary is updated
            employeeModel.EmployeeID = 32;
            decimal result= repository.ReadingUpdatedSalaryFromDataBase(employeeModel);
            Console.WriteLine("Salary\t"+ result);
        }
        /// <summary>
        /// Retrievings the specific date range data.
        /// </summary>
        public static void RetrievingSpecificDateRangeData()
        {
            //instatiating employee repository class
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<EmployeeModel> list = new List<EmployeeModel>();
            try
            {
                //getting list of details from method of getting employee details in date range
                list = employeeRepository.GetAllemployeeInDateRange();
                foreach (EmployeeModel employees in list)
                {
                    Console.WriteLine($"Employee Id :{employees.EmployeeID}");
                    Console.WriteLine($"Employee Name :{employees.EmployeeName}");
                    Console.WriteLine($"Employee Salary :{employees.BasicPay}");
                    Console.WriteLine($"Employee startdate :{employees.StartDate}");
                    Console.WriteLine($"Employee Deductions :{employees.Deductions}");
                    Console.WriteLine($"Employee Taxable Pay :{employees.TaxablePay}");
                    Console.WriteLine($"Tax:{employees.Tax}");
                    Console.WriteLine($"Net pay:{employees.NetPay}");

                }
            }
            //catching exceptions
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Retrievings the grouped data by gender.
        /// </summary>
        public static void RetrievingGroupedDataByGender()
        {
            //instatiating employee repository class
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<EmployeeModel> list = new List<EmployeeModel>();
            try
            {
                //getting list of details of grouped data by gender
                list = employeeRepository.GetGroupedData();
                foreach (EmployeeModel employees in list)
                {
                    Console.WriteLine($"Gender: {employees.Gender} totalSum: {employees.totalSalary} MaxSalary: {employees.maxSalary} ");

                }
            }
            //catching exceptions
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
        public static void AddingDataInMultipleTable()
        {
            //initializing employeemodel
            EmployeeModel employeeModel = new EmployeeModel();
            //adding values to variables
            employeeModel.EmployeeID = 32;
            employeeModel.EmployeeName = "Akshay Poriya";
            employeeModel.Gender = "M";
            employeeModel.PhoneNumber = 9585858585;
            employeeModel.StartDate = Convert.ToDateTime("2020-09-18");
            employeeModel.BasicPay = 500000;
            employeeModel.Deductions = 50000;
            employeeModel.TaxablePay = 450000;
            employeeModel.Tax = 50000;
            employeeModel.NetPay = 400000;
            employeeModel.Address = "Bangalore";
            employeeModel.companyId = 107;
            employeeModel.salaryid = 16;
            employeeModel.companyName = "Capgemini India";
            employeeModel.departmentid = 11;
            employeeModel.Department = "Developer";
            employeeModel.headOfDepartment = "Mr. Kalyan";
            employeeModel.noOfEmployees = 44;
            //instatiating employee repository
            EmployeeRepository employeeRepository = new EmployeeRepository();
            //passing employee model into method of employee repository class
            bool result = employeeRepository.InsertingDataIntoMultipleTables(employeeModel);
            //printing message on the basis of bool result using ternary condition
            Console.WriteLine(result == true ? "data writtern in database" : "data is not written in database");
        }
    }
}
