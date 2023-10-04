namespace Project.HumanResources.Integration.Authentication.VerifyEmail;

public class VerifyEmailResponse
{
    public string VerificationCodes { get; set; }

    public VerifyEmailResponse(string verificationCodes)
    {
        VerificationCodes = verificationCodes;
    }
}