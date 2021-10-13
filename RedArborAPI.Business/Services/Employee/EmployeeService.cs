using RedArborAPI.Business.Interfaces.EmployeeService;
using RedArborAPI.Domain.Entities.Employee;
using RedArborAPI.DTOs.Employees;
using RedArborAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedArborAPI.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Class Variables

        /// <summary>The employee repository.</summary>
        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        public EmployeeService(IEmployeeRepository repository) : base()
        {
            _employeeRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #region Class Methods

            public async Task<AddEmployeeResponse> AddNewEmployeeAsync(AddEmployeeRequest model)
            {
                // Create the Employee Entity
                var employee = new Employee()
                {
                    CompanyId = model.CompanyId,
                    CreatedOn = model.CreatedOn,
                    DeletedOn = model.DeletedOn,
                    Email = model.Email,
                    Fax = model.Fax,
                    Name = model.Name,
                    Lastlogin = model.Lastlogin,
                    Password = model.Password,
                    PortalId = model.PortalId,
                    RoleId = model.RoleId,
                    StatusId = model.StatusId,
                    Telephone = model.Telephone,
                    UpdatedOn = model.UpdatedOn,
                    Username = model.Username
                };

                await _employeeRepository.AddAsync(CreateNewEmployeeSQLQueryString(employee));

                var response = new AddEmployeeResponse()
                {
                    Id = employee.Id,
                    Name = employee.Username
                };

                return response;
            }


            public async Task<DeleteEmployeeResponse> DeleteEmployeeByIdAsync(int? id)
            {
                await _employeeRepository.DeleteAsync(DeleteEmployeeSQLQueryString(id));

                var response = new DeleteEmployeeResponse()
                {
                    Id = id
                };

                return response;
            }

        public async Task<UpdateEmployeeResponse> UpdateEmployeeByIdAsync(int? id, UpdateEmployeeRequest model)
        {
            // Create the Employee Entity
            var employee = new Employee()
            {
                CompanyId = model.CompanyId,
                CreatedOn = model.CreatedOn,
                DeletedOn = model.DeletedOn,
                Email = model.Email,
                Fax = model.Fax,
                Name = model.Name,
                Lastlogin = model.Lastlogin,
                Password = model.Password,
                PortalId = model.PortalId,
                RoleId = model.RoleId,
                StatusId = model.StatusId,
                Telephone = model.Telephone,
                UpdatedOn = model.UpdatedOn,
                Username = model.Username
            };

            await _employeeRepository.UpdateAsync(UpdateEmployeeSQLQueryString(employee, id));
            var response = new UpdateEmployeeResponse()
            {
                Id = id,
                Name = employee.Username
            };

            return response;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int? id)
        {
            var employee = _employeeRepository.GetAsync(GetEmployeeByIdSQLQueryString(id));
            var employeeClean = employee.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return createEmployeeEntity(employeeClean);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employeeEntities = new List<Employee>() { };
            var employees = _employeeRepository.ListAsync(GetEmployeesSQLQueryString());

            foreach (var e in employees)
            {
                var employeeClean = e.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var employeeEntity = createEmployeeEntity(employeeClean);
                employeeEntities.Add(employeeEntity);
            }

            return employeeEntities;
        }

        #endregion


        #region Helper Methods

        /// <summary>This method creates the SQl query to add a new employee in the data base.</summary>
        /// <param name="employee">The employee model.</param>
        /// <returns>SQL query for inserting a employee row</returns>
        private string CreateNewEmployeeSQLQueryString(Employee employee)
        {
            return string.Format(string.Concat("INSERT INTO [Employees].[dbo].[Employee]",
                "([CompanyId],[CreatedOn],[DeletedOn],[Email],[Fax],[Name],[Lastlogin],[Password],[PortalId],[RoleId],[StatusId],[Telephone],[UpdatedOn],[Username])",
                "VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9}, {10}, '{11}', '{12}', '{13}')"), employee.CompanyId, employee.CreatedOn, employee.DeletedOn, employee.Email, employee.Fax, employee.Name,
                employee.Lastlogin, employee.Password, employee.PortalId, employee.RoleId, employee.StatusId, employee.Telephone, employee.UpdatedOn, employee.Username);
        }

        /// <summary>This method creates the SQl query to delete an existing employee in the data base.</summary>
        /// <param name="id">The employee id.</param>
        /// <returns>SQL query for deleting a employee row</returns>
        private string DeleteEmployeeSQLQueryString(int? id)
        {
        return "DELETE FROM [Employees].[dbo].[Employee] WHERE CompanyId = " + id;
        }

        /// <summary>This method creates the SQl query to get an existing employee in the data base by id.</summary>
        /// <param name="id">The employee id.</param>
        /// <returns>SQL query for deleting a employee row</returns>
        private string GetEmployeeByIdSQLQueryString(int? id)
        {
            return "SELECT * FROM [Employees].[dbo].[Employee] WHERE CompanyId = " + id;
        }

        /// <summary>This method creates the SQl query to get ALL existing employees.</summary>
        /// <returns>SQL query for deleting a employee row</returns>
        private string GetEmployeesSQLQueryString()
        {
            return "SELECT * FROM [Employees].[dbo].[Employee]";
        }


        /// <summary>This method creates the SQl query to update an existing employee in the data base.</summary>
        /// <param name="id">The employee id.</param>
        /// <returns>SQL query for deleting a employee row</returns>
        private string UpdateEmployeeSQLQueryString(Employee employee, int? id)
        {
            return string.Format("UPDATE [Employees].[dbo].[Employee] SET [CompanyId] = {0} ,[CreatedOn] = '{1}', [DeletedOn] = '{2}', [Email] = '{3}', [Fax] = '{4}', [Name] = '{5}', [Lastlogin] = '{6}', " +
                "[Password] = '{7}', [PortalId] = {8}, [RoleId] = {9}, [StatusId] = {10}, [Telephone] = '{11}', [UpdatedOn] = '{12}', [Username] = '{13}' " +
                "WHERE CompanyId = {14}", employee.CompanyId, employee.CreatedOn, employee.DeletedOn, employee.Email, employee.Fax, employee.Name, employee.Lastlogin, employee.Password, employee.PortalId,
                employee.RoleId, employee.StatusId, employee.Telephone, employee.UpdatedOn, employee.Username, id);
        }

        /// <summary>This method creates an Employee entity.</summary>
        /// <param name="employeeFromDb">The employee string from data base.</param>
        /// <returns>Employee entity data structure.</returns>
        private Employee createEmployeeEntity(List<string> employeeFromDb)
        {
            return new Employee()
            {
                CompanyId = int.Parse(employeeFromDb[0]),
                CreatedOn = Convert.ToDateTime(employeeFromDb[1].Replace("'", string.Empty).Trim()),
                DeletedOn = Convert.ToDateTime(employeeFromDb[1].Replace("'", string.Empty).Trim()),
                Email = employeeFromDb[3].Replace("'", string.Empty).Trim(),
                Fax = employeeFromDb[4].Replace("'", string.Empty).Trim(),
                Name = employeeFromDb[5].Replace("'", string.Empty).Trim(),
                Lastlogin = Convert.ToDateTime(employeeFromDb[1].Replace("'", string.Empty).Trim()),
                Password = employeeFromDb[7].Replace("'", string.Empty).Trim(),
                PortalId = int.Parse(employeeFromDb[8]),
                RoleId = int.Parse(employeeFromDb[9]),
                StatusId = int.Parse(employeeFromDb[10]),
                Telephone = employeeFromDb[11].Replace("'", string.Empty).Trim(),
                UpdatedOn = Convert.ToDateTime(employeeFromDb[1].Replace("'", string.Empty).Trim()),
                Username = employeeFromDb[13].Replace("'", string.Empty).Trim()
            };
        }

        #endregion
    }
}
