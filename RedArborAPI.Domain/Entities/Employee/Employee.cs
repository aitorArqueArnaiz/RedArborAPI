using Newtonsoft.Json;
using RedArborAPI.Domain.Base;
using System;
using static RedArborAPI.Domain.Shared.Enums;

namespace RedArborAPI.Domain.Entities.Employee
{
    public class Employee : BaseEntity
    {
        /*
         * Example of employee DTO
             * {
                "CompanyId": 1,
                "CreatedOn": "2000-01-01 00:00:00",
                "DeletedOn": "2000-01-01 00:00:00",
                "Email": "test1@test.test.tmp",
                "Fax": "000.000.000",
                "Name": "test1",
                "Lastlogin": "2000-01-01 00:00:00",
                "Password": "test",
                "PortalId": 1,
                "RoleId": 1,
                "StatusId": 1,
                "Telephone": "000.000.000",
                "UpdatedOn": "2000-01-01 00:00:00",
                "Username": "test1"
              }
         */
        public Employee()
        {
            this.CompanyId = int.MinValue;
            this.CreatedOn = string.Empty;
            this.DeletedOn = string.Empty;
            this.Email = string.Empty;
            this.Fax = string.Empty;
            this.Name = string.Empty;
            this.Lastlogin = string.Empty;
            this.Password = string.Empty;
            this.PortalId = int.MinValue;
            this.RoleId = int.MinValue;
            this.StatusId = EmployeeStatus.Contracted;
            this.Telephone = string.Empty;
            this.UpdatedOn = string.Empty;
            this.Username = string.Empty;
        }

        [JsonProperty("CompanyId")]
        public int CompanyId { get;  set; }

        [JsonProperty("CreatedOn")]
        public string CreatedOn { get;  set; }

        [JsonProperty("DeletedOn")]
        public string DeletedOn { get;  set; }

        [JsonProperty("Email")]
        public string Email { get;  set; }

        [JsonProperty("Fax")]
        public string Fax { get;  set; }

        [JsonProperty("Name")]
        public string Name { get;  set; }

        [JsonProperty("Lastlogin")]
        public string Lastlogin { get;  set; }

        [JsonProperty("Password")]
        public string Password { get;  set; }

        [JsonProperty("PortalId")]
        public int PortalId { get;  set; }

        [JsonProperty("RoleId")]
        public int RoleId { get;  set; }

        [JsonProperty("StatusId")]
        public EmployeeStatus StatusId { get;  set; }

        [JsonProperty("Telephone")]
        public string Telephone { get;  set; }

        [JsonProperty("UpdatedOn")]
        public string UpdatedOn { get;  set; }

        [JsonProperty("Username")]
        public string Username { get;  set; }
    }
}
