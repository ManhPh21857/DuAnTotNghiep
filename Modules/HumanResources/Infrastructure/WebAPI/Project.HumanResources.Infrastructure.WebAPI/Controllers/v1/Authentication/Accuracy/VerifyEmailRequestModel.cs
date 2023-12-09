namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Accuracy
{
    public class VerifyEmailRequestModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
