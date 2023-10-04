namespace Project.Core.Infrastructure.WebAPI.Models
{
    public enum BadRequestType
    {
        BodyEmpty = 0,
        FieldRequired = 1,
        TypeMismatch = 2,
        ParamNotValid = 3,
    }
}