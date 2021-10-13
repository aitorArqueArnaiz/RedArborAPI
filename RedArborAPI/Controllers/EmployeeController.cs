using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedArborAPI.Business.Interfaces.EmployeeService;
using RedArborAPI.Domain.Entities.Employee;
using RedArborAPI.DTOs.Employees;
using System;

namespace RedArborAPI.Controllers
{
    [ApiController]
    [Route("api/redarbor")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger,
                                  IEmployeeService service)
        {
            _logger = logger;
            _employeeService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeByIdController(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var employeeResponse = _employeeService.GetEmployeeByIdAsync(id);
                return Ok($"Employee with id : {id} has been succesfully found. Json with employe information is {JsonConvert.SerializeObject(employeeResponse.Result, Formatting.Indented)}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeById action: {ex.Message}");
                return StatusCode(500, $"Something went wrong inside GetEmployeeById action: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllEmployeesController()
        {
            try
            {
                var employee = _employeeService.GetAllEmployeesAsync();
                return Ok($"Json with employees information is {JsonConvert.SerializeObject(employee.Result, Formatting.Indented)}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllEmployees action: {ex.Message}");
                return StatusCode(500, $"Something went wrong inside GetAllEmployees action: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeController([FromBody] Employee e)
        {
            try
            {
                if (e == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }
                else if ((int)e.StatusId < 1 || (int)e.StatusId > 3)
                {
                    _logger.LogError("Employee status is incorrect.");
                    return BadRequest("Employee status is incorrect");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Call and process the logics for adding a new employee
                UpdateEmployeeRequest employeeRequest = new UpdateEmployeeRequest()
                {
                    CompanyId = e.CompanyId,
                    CreatedOn = e.CreatedOn,
                    DeletedOn = e.DeletedOn,
                    Email = e.Email,
                    Fax = e.Fax,
                    Name = e.Name,
                    Lastlogin = e.Lastlogin,
                    Password = e.Password,
                    PortalId = e.PortalId,
                    RoleId = e.RoleId,
                    StatusId = (int)e.StatusId,
                    Telephone = e.Telephone,
                    UpdatedOn = e.UpdatedOn,
                    Username = e.Username
                };

                var addEmployeeResponse = _employeeService.UpdateEmployeeByIdAsync(e.CompanyId, employeeRequest);
                return Ok($"Employee {e.Username} has been succesfully updated. Response Id : {addEmployeeResponse.Result.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateEmployee action: {ex.Message}");
                return StatusCode(500, $"Something went wrong inside UpdateEmployee action: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeController(int? id)
        {
            try
            {
                _employeeService.DeleteEmployeeByIdAsync(id);
                return Ok($"Employee with id : {id} has been succesfully removed");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, $"Something went wrong inside DeleteEmployee action: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Employee> AddEmployeeController([FromBody] Employee e)
        {
            try
            {
                if (e == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                // Call and process the logics for adding a new employee
                AddEmployeeRequest employeeRequest = new AddEmployeeRequest()
                {
                    CompanyId = e.CompanyId,
                    CreatedOn = e.CreatedOn,
                    DeletedOn = e.DeletedOn,
                    Email = e.Email,
                    Fax = e.Fax,
                    Name = e.Name,
                    Lastlogin = e.Lastlogin,
                    Password = e.Password,
                    PortalId = e.PortalId,
                    RoleId = e.RoleId,
                    StatusId = (int)e.StatusId,
                    Telephone = e.Telephone,
                    UpdatedOn = e.UpdatedOn,
                    Username = e.Username
                };

                var addEmployeeResponse = _employeeService.AddNewEmployeeAsync(employeeRequest);
                return Ok($"Employee {e.Username} has been succesfully added. Response Id : {addEmployeeResponse.Result.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddEmployee action: {ex.Message}");
                return StatusCode(500, $"Something went wrong inside AddEmployee action: {ex.Message}");
            }
        }
    }
}
