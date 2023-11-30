namespace Project.HumanResources.Integration.Employees.Command
{
    public class UpdateEmployeeCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateEmployeeCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
