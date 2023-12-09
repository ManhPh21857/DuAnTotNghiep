namespace Project.HumanResources.Integration.Authentication.Forgot
{
    public class ForgotResponse
    {
        public bool IsSuccess { get; set; }

        public ForgotResponse(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
