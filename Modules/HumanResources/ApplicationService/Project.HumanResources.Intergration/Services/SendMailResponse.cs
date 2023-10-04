namespace Project.HumanResources.Integration.Services;

public class SendMailResponse
{
    public bool IsSuccess { get; set; }

    public SendMailResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}