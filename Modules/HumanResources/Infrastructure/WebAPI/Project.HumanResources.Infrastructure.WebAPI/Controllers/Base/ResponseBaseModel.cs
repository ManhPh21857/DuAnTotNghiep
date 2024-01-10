namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;

public class ResponseBaseModel<T>
{
    public bool Status { get; set; }

    public ResponseBaseModel()
    {
        this.Status = true;
    }

    public ResponseBaseModel(T? data) : this()
    {
        this.Data = data;
    }

    public T? Data { get; set; }
}