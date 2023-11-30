namespace Project.HumanResources.Integration.Employees.Login
{
    public class EmployeeLoginResponse
    {
        public string Result { get; set; }

        public EmployeeLoginResponse(string result)
        {
            Result = result;
        }
    }
}
