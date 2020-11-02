// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Capgemini">
//   Copyright
// </copyright>
// <creator Name="Vishal Garg"/>
// --------------------------------------------------------------------------------------------------------------------

namespace ADO.NETDemo
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        EmployeeModel employeeModel = new EmployeeModel();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll");

            
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<EmployeeModel> list = employeeRepository.GetAllemployee();
            try
            {
                foreach (EmployeeModel employeeModel in list)
                {
                    Console.WriteLine($"Id: {employeeModel.EmployeeID} Name:{employeeModel.EmployeeName} CompanyName: {employeeModel.companyName} DepartmentName: {employeeModel.Department} phoneNumber: {employeeModel.PhoneNumber} gender: {employeeModel.Gender}  address: {employeeModel.Address} netpay={employeeModel.NetPay}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
    }
}
