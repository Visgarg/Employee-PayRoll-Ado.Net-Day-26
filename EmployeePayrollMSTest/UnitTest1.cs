using NUnit.Framework;
using ADO.NETDemo;

namespace EmployeePayrollMSTest
{
    public class Tests
    {
        /// <summary>
        /// Givens the salary details able to update salary details.
        /// </summary>
        [Test]
        public void GivenSalaryDetails_AbleToUpdateSalaryDetails()
        {
            //instatiating employee repository class
            EmployeeRepository repository = new EmployeeRepository();
            //instatiating employeemodel class
            EmployeeModel model = new EmployeeModel();
            //defining values to variables that will be passed to stored procedure in update salary method
            model.EmployeeID = 1;
            model.EmployeeName = "Vishal";
            model.BasicPay = 2000;
            //passing model in updating salary method
            repository.UpdatingSalaryInDataBase(model);
            //reading updated salary from method
            decimal actual = repository.ReadingUpdatedSalaryFromDataBase();
            //checking if both the values are equal
            Assert.AreEqual(model.BasicPay, actual);
        }
        
    }
}