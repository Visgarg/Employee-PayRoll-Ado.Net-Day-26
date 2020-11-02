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
    /// <summary>
    /// employee repository class to connect to database
    /// </summary>
    class EmployeeRepository
    {
        //static as connection will be made only once in application
        //list for saving details from database
        List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
        //connectionstring defines connection to be made to database
        public static string connectionString = @"Data Source=DESKTOP-ERFDFCL\SQLEXPRESS01;Initial Catalog=employee_payroll;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //connection is made
        SqlConnection connection = new SqlConnection(connectionString);
        /// <summary>
        /// Gets the allemployee from database
        /// </summary>
        /// <returns>list of all the database details</returns>
        /// <exception cref="Exception">No data found</exception>
        public List<EmployeeModel> GetAllemployee()
        {
            using (connection)
            {
                //sql query
                string query = "select * from employee e join payroll p on e.id = p.employeeID join EmployeeDepartment ed on ed.employeeID = e.id join company c on c.company_id = e.company_id join departments d on d.departmentID = ed.departmentID ";
                //sql command
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                //opening up connection
                connection.Open();
                //reading data from database
                //connected architecture
                SqlDataReader dr = sqlCommand.ExecuteReader();
                //if database has rows then if condition is satisfied
                if (dr.HasRows)
                {
                    //runs upto reading of data
                    while (dr.Read())
                    {
                        //all the data needs to be iterated, mapping is done hence no is specified into getint or getstring.
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

                        //adding details into list
                        employeeDetailsList.Add(employeeModel);
                        
                       
                    }
                    //reader connection closed
                    dr.Close();
                    //database connection closed
                    connection.Close();
                    //returning list
                    return employeeDetailsList;
                }
                else
                {
                    //throw exception if data not found
                    throw new Exception("No data found");
                }

            }
        }
        /// <summary>
        /// Adds the employee data into database
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                //using established connection
                using(this.connection)
                {
                    //sql command which executes stored procedure created in sql server
                    SqlCommand command = new SqlCommand("spAddEmployeeDetails", connection);
                    //commandtype is choosen as stored procedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //adding data into variables defined in stored procedure
                    command.Parameters.AddWithValue("@Employeename", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@phoneNumber", employeeModel.PhoneNumber);
                    command.Parameters.AddWithValue("@address", employeeModel.Address);
                    command.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    command.Parameters.AddWithValue("@companyid", employeeModel.companyId);
                    command.Parameters.AddWithValue("@start", employeeModel.StartDate);
                    //opening connection
                    connection.Open();
                    //adding data into database - using disconnected architecture(as connected architecture only reads the data)
                    var result= command.ExecuteNonQuery();
                    //closing connection
                    connection.Close();
                    if(result !=0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
