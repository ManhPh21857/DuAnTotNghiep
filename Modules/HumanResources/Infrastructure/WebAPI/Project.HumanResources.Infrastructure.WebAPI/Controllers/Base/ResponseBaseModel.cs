namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;

public class ResponseBaseModel<T>
{
    public bool Status { get; set; }

    public ResponseBaseModel()
    {
        Status = true;
    }

    public ResponseBaseModel(T? data) : this()
    {
        Data = data;
    }

    public T? Data { get; set; }
}