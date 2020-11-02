// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Capgemini">
//   Copyright
// </copyright>
// <creator Name="Vishal Garg"/>
// --------------------------------------------------------------------------------------------------------------------


namespace ADO.NETDemo
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq.Expressions;
    using System.Text;
    class EmployeeRepository
    {
        //static as connection will be made only once in application
        List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
        public static string connectionString = @"Data Source=DESKTOP-ERFDFCL\SQLEXPRESS01;Initial Catalog=employee_payroll;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);

        public List<EmployeeModel> GetAllemployee()
        {
           
            using (connection)
            {
                string query = "select * from employee e join payroll p on e.id = p.employeeID join EmployeeDepartment ed on ed.employeeID = e.id join company c on c.company_id = e.company_id join departments d on d.departmentID = ed.departmentID ";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmployeeModel employeeModel = new EmployeeModel();
                        employeeModel.EmployeeID = dr.GetInt32(0);
                        employeeModel.EmployeeName = dr.GetString(1);
                        employeeModel.StartDate = dr.GetDateTime(2);
                        employeeModel.Gender = dr.GetString(3);
                        employeeModel.PhoneNumber = dr.GetInt64(4);
                        employeeModel.Address = dr.GetString(5);
                        employeeModel.companyId = dr.GetInt32(6);
                        employeeModel.BasicPay = dr.GetDecimal(8);
                        employeeModel.Deductions = dr.GetDecimal(9);
                        employeeModel.TaxablePay = dr.GetDecimal(10);
                        employeeModel.Tax = dr.GetDecimal(11);
                        employeeModel.NetPay = dr.GetDecimal(12);
                        employeeModel.companyName = dr.GetString(16);
                        employeeModel.Department = dr.GetString(18);

                        //display retrieved record
                        employeeDetailsList.Add(employeeModel);
                        
                       
                    }
                    dr.Close();
                    connection.Close();
                    return employeeDetailsList;
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
        }
    }
}
