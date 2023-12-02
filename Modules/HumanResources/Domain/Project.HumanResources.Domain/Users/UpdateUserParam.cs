namespace Project.HumanResources.Domain.Users
{
    public class UpdateUserParam
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
