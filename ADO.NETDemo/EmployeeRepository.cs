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
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq.Expressions;
    using System.Text;
    /// <summary>
    /// employee repository class to connect to database
    /// </summary>
    public class EmployeeRepository
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
                string query = "select * from employee e join payroll p on e.salaryid = p.salary_Id join EmployeeDepartment ed on ed.employeeID = e.id join company c on c.company_id = e.company_id join departments d on d.departmentID = ed.departmentID ";
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
                        employeeModel.companyName = dr.GetString(17);
                        employeeModel.Department = dr.GetString(19);

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
                using (this.connection)
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
                    command.Parameters.AddWithValue("@salaryid",employeeModel.salaryid);
                    //opening connection
                    connection.Open();
                    //adding data into database - using disconnected architecture(as connected architecture only reads the data)
                    var result = command.ExecuteNonQuery();
                    //closing connection
                    connection.Close();
                    if (result != 0)
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
        /// <summary>
        /// Updatings the salary in data base for a employee
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool UpdatingSalaryInDataBase(EmployeeModel employeeModel)
        {
            try
            {
               //using the connection defined at start of class 
                using (this.connection)
                {
                    //SqlCommand contains sql stored procedure for updating salary and a connection
                    SqlCommand sqlCommand = new SqlCommand("spUpdatingSalary", connection);
                    //command type is for stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    //parameters are assigned values from employeeModel
                    sqlCommand.Parameters.AddWithValue("@id", employeeModel.EmployeeID);
                    sqlCommand.Parameters.AddWithValue("@salary", employeeModel.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@name", employeeModel.EmployeeName);
                    //opening up connection
                    connection.Open();
                    //result contain no of affected rows as Execute Non Query gives no of affected rows after query
                    int result= sqlCommand.ExecuteNonQuery();
                    if(result!=0)
                    {
                        return true;
                    }
                    return false;
              
                }

            }
            //catching up the exception 
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Readings the updated salary from data base after updating data.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">no data found</exception>
        public decimal ReadingUpdatedSalaryFromDataBase()
        {
            //defining connection string and connection seperately for reading of connection string and connection by unit test
            string connectionString1 = @"Data Source=DESKTOP-ERFDFCL\SQLEXPRESS01;Initial Catalog=employee_payroll;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection1 = new SqlConnection(connectionString1);
           // using (this.connection1)
            //{
                //variable defined for passing salary to program.cs class
                decimal salary;
                //employee model class is instantiated to read the data
                EmployeeModel model = new EmployeeModel();
                //sql command consisting of employee payroll
                SqlCommand sqlCommand = new SqlCommand("Select * from employee_payroll", connection1);
                //opening up connection
                connection1.Open();
                //reading up the data from database using connected architecture
                SqlDataReader dr = sqlCommand.ExecuteReader();
                //if datareader has any rows then while loop is executed until data is read line by line
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.EmployeeID = Convert.ToInt32(dr["id"]);
                        model.EmployeeName = dr["name"].ToString();
                        model.BasicPay = Convert.ToDecimal(dr["salary"]);
                    }
                    Console.WriteLine($"employeeId :{model.EmployeeID}, employeename: {model.EmployeeName}, salary :{model.BasicPay}");
                    salary = model.BasicPay;

                }
                else
                {
                    throw new Exception("no data found");
                }
                //closing up reading and connection.
                dr.Close();
                connection1.Close();
                //return updated salary
                return salary;
           // }
            
        }
        public List<EmployeeModel> GetAllemployeeInDateRange()
        {
            using (connection)
            {
                //sql query
                string query = "select * from employee_payroll where start between cast('2019-03-03' as date) and getdate();";
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
                        employeeModel.BasicPay = dr.GetDecimal(2);
                        employeeModel.StartDate = dr.GetDateTime(3);
                        employeeModel.Gender = dr.GetString(4);
                        employeeModel.Deductions = dr.GetDecimal(5);
                        employeeModel.TaxablePay = dr.GetDecimal(6);
                        employeeModel.Tax = dr.GetDecimal(7);
                        employeeModel.NetPay = dr.GetDecimal(8);

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
        /// Gets the grouped data from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">No data found</exception>
        public List<EmployeeModel> GetGroupedData()
        {
            using (connection)
            {
                //sql query
                string query = "select gender, sum(salary) total_sum, max(salary) max_salary, min(salary) min_salary, AVG(salary) avg_salary, count(salary) CountOfGenders from employee_payroll group by gender";
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
                        employeeModel.Gender = dr["Gender"].ToString();
                        employeeModel.totalSalary = dr.GetDecimal(1);
                        //passing aliased name
                        employeeModel.maxSalary = Convert.ToDecimal(dr["max_salary"]);

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
        /// Insertings the data into multiple tables.
        /// </summary>
        /// <param name="employeeModel">The employee model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool InsertingDataIntoMultipleTables(EmployeeModel employeeModel)
        {
            try
            {
                //using established connection
                using (this.connection)
                {
                    //sql command which executes stored procedure created in sql server
                    //inserting data stored procedure implements transaction
                    //if data is added wrong in any table, whole data will be rolled back.
                    SqlCommand command = new SqlCommand("insertingdata", connection);
                    //commandtype is choosen as stored procedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    //adding data into variables defined in stored procedure
                    command.Parameters.AddWithValue("@Employeeid", employeeModel.EmployeeID);
                    command.Parameters.AddWithValue("@phone_number", employeeModel.PhoneNumber);
                    command.Parameters.AddWithValue("@address", employeeModel.Address);
                    command.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    command.Parameters.AddWithValue("@company_id", employeeModel.companyId);
                    command.Parameters.AddWithValue("@start", employeeModel.StartDate);
                    command.Parameters.AddWithValue("@salaryid", employeeModel.salaryid);
                    command.Parameters.AddWithValue("@basepay", employeeModel.BasicPay);
                    command.Parameters.AddWithValue("@deductions", employeeModel.Deductions);
                    command.Parameters.AddWithValue("@taxable_pay", employeeModel.TaxablePay);
                    command.Parameters.AddWithValue("@tax", employeeModel.Tax);
                    command.Parameters.AddWithValue("@netpay", employeeModel.NetPay);
                    command.Parameters.AddWithValue("@name", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@departmentid", employeeModel.departmentid);
                    command.Parameters.AddWithValue("@departmentname", employeeModel.Department);
                    command.Parameters.AddWithValue("@noOfEmployees", employeeModel.noOfEmployees);
                    command.Parameters.AddWithValue("@headofdepartment", employeeModel.headOfDepartment);
                    command.Parameters.AddWithValue("@companyname", employeeModel.companyName);
                    //opening connection
                    connection.Open();
                    //adding data into database - using disconnected architecture(as connected architecture only reads the data)
                    var result = command.ExecuteNonQuery();
                    //closing connection
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
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