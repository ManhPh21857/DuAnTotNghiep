﻿using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Domain.Employees
{
    public class EmployeeUser
    {
        public int Id { get; set; }
        public string? UID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? EmployeeDataVersion { get; set; }
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public byte[]? UserDataVersion { get; set; }
        public IEnumerable<RoleInfo> Roles { get; set; }

        public EmployeeUser()
        {
            this.Roles = new List<RoleInfo>();
        }
    }
}
