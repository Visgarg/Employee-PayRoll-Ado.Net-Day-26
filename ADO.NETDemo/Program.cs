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
    /// <summary>
    /// Calling different methods to print list, add details
    /// </summary>
    class Program
    {
        //instatiating employee model
        EmployeeModel employeeModel = new EmployeeModel();
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Console.WriteLine("Welcome to Employee Payroll");
            //GettingAllData();
            //AddingDataInDataBase();
           // EmployeeRepositoryCaller.UpdatingSalary();
            //EmployeeRepositoryCaller.ReadingUpdatedData();
            //RetrievingSpecificDateRangeData();
            EmployeeRepositoryCaller.RetrievingGroupedDataByGender();
            //EmployeeRepositoryCaller.AddingDataInMultipleTable();

        }
       
    }
}