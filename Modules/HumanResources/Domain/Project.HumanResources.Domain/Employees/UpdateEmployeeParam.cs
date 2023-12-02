namespace Project.HumanResources.Domain.Employees
{
    public class UpdateEmployeeParam
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
