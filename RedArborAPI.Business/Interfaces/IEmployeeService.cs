
using RedArborAPI.Domain.DTOs.Employees;
using RedArborAPI.DTOs.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedArborAPI.Business.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>This method adds a new employee into the data base.</summary>
        /// <param name="model">The employee dto from the controller.</param>
        Task<AddEmployeeResponse> AddNewEmployeeAsync(AddEmployeeRequest model);

        /// <summary>This method deletes a new employee into the data base.</summary>
        /// <param name="id">The employee id.</param>
        Task<DeleteEmployeeResponse> DeleteEmployeeByIdAsync(int? id);

        /// <summary>This method updates existing employee.</summary>
        /// <param name="id">The employee id.</param>
        Task<UpdateEmployeeResponse> UpdateEmployeeByIdAsync(int? id, UpdateEmployeeRequest model);

        /// <summary>This method Gets an employee by his id.</summary>
        /// <param name="id">The employee id.</param>
        Task<Employee> GetEmployeeByIdAsync(int? id);

        /// <summary>This method gets all employees.</summary>
        /// <param name="id">The employee id.</param>
        Task<List<Employee>> GetAllEmployeesAsync();
    }
}
