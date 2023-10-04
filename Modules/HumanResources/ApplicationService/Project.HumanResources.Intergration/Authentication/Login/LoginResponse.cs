namespace Project.HumanResources.Integration.Authentication.Login;

public class LoginResponse
{
    public string Result { get; set; }

    public LoginResponse(string result)
    {
        Result = result;
    }
}