namespace Project.HumanResources.Integration.Employees.Command
{
    public class DeleteEmployeeCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteEmployeeCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
