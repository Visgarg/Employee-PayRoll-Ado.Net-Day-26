// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="Capgemini">
//   Copyright
// </copyright>
// <creator Name="Vishal Garg"/>
// --------------------------------------------------------------------------------------------------------------------
namespace ADO.NETDemo
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public decimal BasicPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal TaxablePay { get; set; } 
        public decimal Tax { get; set; }
        public DateTime StartDate { get; set; }
        public string city { get; set; }
        public string Country { get; set; }
        public string companyName { get; set; }
        public int companyId { get; set; }
        public decimal NetPay { get; set; }
        public int salaryid { get; set; }

        public decimal totalSalary { get; set; }
        public decimal maxSalary { get; set; }
    }
}
