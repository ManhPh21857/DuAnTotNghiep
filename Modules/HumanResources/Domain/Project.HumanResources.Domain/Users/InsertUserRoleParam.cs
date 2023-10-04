using Project.Core.Domain.Enums;

namespace Project.HumanResources.Domain.Users;

public class InsertUserRoleParam
{
    public int UserId { get; set; }
    public Role Role { get; set; }
}