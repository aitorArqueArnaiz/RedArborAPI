using Newtonsoft.Json;
using RedArborAPI.Domain.Base;
using System;

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
            this.CreatedOn = new DateTime().Date;
            this.DeletedOn = new DateTime().Date;
            this.Email = string.Empty;
            this.Fax = string.Empty;
            this.Name = string.Empty;
            this.Lastlogin = new DateTime().Date;
            this.Password = string.Empty;
            this.PortalId = int.MinValue;
            this.RoleId = int.MinValue;
            this.StatusId = int.MinValue;
            this.Telephone = string.Empty;
            this.UpdatedOn = new DateTime().Date;
            this.Username = string.Empty;
        }

        [JsonProperty("CompanyId")]
        public int CompanyId { get;  set; }

        [JsonProperty("CreatedOn")]
        public DateTime? CreatedOn { get;  set; }

        [JsonProperty("DeletedOn")]
        public DateTime? DeletedOn { get;  set; }

        [JsonProperty("Email")]
        public string Email { get;  set; }

        [JsonProperty("Fax")]
        public string Fax { get;  set; }

        [JsonProperty("Name")]
        public string Name { get;  set; }

        [JsonProperty("Lastlogin")]
        public DateTime? Lastlogin { get;  set; }

        [JsonProperty("Password")]
        public string Password { get;  set; }

        [JsonProperty("PortalId")]
        public int PortalId { get;  set; }

        [JsonProperty("RoleId")]
        public int RoleId { get;  set; }

        [JsonProperty("StatusId")]
        public int StatusId { get;  set; }

        [JsonProperty("Telephone")]
        public string Telephone { get;  set; }

        [JsonProperty("UpdatedOn")]
        public DateTime? UpdatedOn { get;  set; }

        [JsonProperty("Username")]
        public string Username { get;  set; }
    }
}
