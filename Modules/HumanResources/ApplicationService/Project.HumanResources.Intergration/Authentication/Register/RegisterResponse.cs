namespace Project.HumanResources.Integration.Authentication.Register;

public class RegisterResponse
{
    public bool Result { get; set; }

    public RegisterResponse(bool result)
    {
        Result = result;
    }
}