namespace Project.Core.Domain;

public class DomainException : ApplicationException
{
    public string ErrorCode { get; }
    public string ErrorMessage { get; }
    public string ErrorMessageKey { get; }
    public IDictionary<string, string> ErrorMessageParam;

    public DomainException(
        string errorCode,
        string errorMessage
    ) : base(errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorMessageKey = errorMessage;
        ErrorMessageParam = new Dictionary<string, string>();
    }

    public DomainException(
        string errorCode,
        string errorMessage,
        string errorMessageKey
    ) : this(errorCode, errorMessage)
    {
        ErrorMessageKey = errorMessageKey;
        ErrorMessageParam = new Dictionary<string, string>();
    }

    public DomainException(
        string errorCode,
        string errorMessage,
        string errorMessageKey,
        IDictionary<string, string> errorMessageParam
    ) : this(errorCode, errorMessage)
    {
        ErrorMessageKey = errorMessageKey;
        ErrorMessageParam = errorMessageParam;
    }

    public void AddMessageParam(string key, string value)
    {
        ErrorMessageParam.Add(key, value);
    }

    public override string ToString()
    {
        return $"{ErrorCode}: {ErrorMessage}";
    }
}