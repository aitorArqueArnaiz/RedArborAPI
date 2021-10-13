using Moq;
using NUnit.Framework;
using RedArborAPI.Business.Interfaces.EmployeeService;
using RedArborAPI.Business.Services;
using RedArborAPI.Domain.Entities.Employee;
using RedArborAPI.DTOs.Employees;
using RedArborAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static RedArborAPI.Domain.Shared.Enums;

namespace RedArborAPI.UnitTests.Test
{
    public class RedArborTest
    {

        #region Class Variables/Properties

            private Mock<IEmployeeRepository> _employeeRepository;
            private IEmployeeService _employeeService;

        #endregion

        #region Class Setup Methods

        [SetUp]
            public void Setup()
            {
                _employeeRepository = new Mock<IEmployeeRepository>();
                _employeeService = new EmployeeService(_employeeRepository.Object);
            }

            [TearDown]
            public void TearDown()
            {
                _employeeService = null;
            }


        #endregion

        #region Class Tests

            [Test]
            [Description("Test intended to reproduce the service flow for adding a new employee entity")]
            [Author("Aitor Arqué Arnaiz")]
            public void AddEmployeeEntity()
            {
                // Arrange
                Random random = new Random();
                AddEmployeeRequest employeeModel = new AddEmployeeRequest()
                {
                    CompanyId = random.Next(0, 9999),
                    CreatedOn = new DateTime().ToUniversalTime(),
                    DeletedOn = new DateTime().ToUniversalTime(),
                    Email = "test@gmail.com",
                    Fax = "123.456.789",
                    Name = "test",
                    Lastlogin = new DateTime().ToUniversalTime(),
                    Password = "123456789",
                    PortalId = 9,
                    RoleId = 6,
                    StatusId = 1,
                    Telephone = "569347640",
                    UpdatedOn = new DateTime().ToUniversalTime(),
                    Username = "test user"
                };

                // Act
                Task<AddEmployeeResponse> addEmployeeResponse = _employeeService.AddNewEmployeeAsync(employeeModel);

                // Assert
                Assert.NotNull(addEmployeeResponse);
                Assert.AreEqual(addEmployeeResponse.Result.Id, 0);
                Assert.AreEqual(addEmployeeResponse.Result.Name, "test user");
            }

            [Test]
            [Description("Test intended to reproduce the service flow for deleting an existing employee entity")]
            [Author("Aitor Arqué Arnaiz")]
            public void DeleteEmployeeEntity()
            {
                // Arrange
                DeleteEmployeeRequest employeeModel = new DeleteEmployeeRequest()
                {
                    Id = new Random(100).Next(),
                };

                // Act
                Task<DeleteEmployeeResponse> deleteEmployeeResponse = _employeeService.DeleteEmployeeByIdAsync(employeeModel.Id);

                // Assert
                Assert.NotNull(deleteEmployeeResponse);
                Assert.AreEqual(deleteEmployeeResponse.Result.Id, employeeModel.Id);
            }

            [Test]
            [Description("Test intended to reproduce the service flow for updating an existing employee entity")]
            [Author("Aitor Arqué Arnaiz")]
            public void UpdateEmployeeEntity()
            {
                // Arrange
                Random random = new Random();
                UpdateEmployeeRequest employeeModel = new UpdateEmployeeRequest()
                {
                    CompanyId = random.Next(0, 9999),
                    CreatedOn = new DateTime().ToUniversalTime(),
                    DeletedOn = new DateTime().ToUniversalTime(),
                    Email = "test@gmail.com",
                    Fax = "503.576.579",
                    Name = "test",
                    Lastlogin = new DateTime().ToUniversalTime(),
                    Password = "123456789",
                    PortalId = 3,
                    RoleId = 3,
                    StatusId = 0,
                    Telephone = "569347640",
                    UpdatedOn = new DateTime().ToUniversalTime(),
                    Username = "test user"
                };

                // Act
                Task<UpdateEmployeeResponse> updateEmployeeResponse = _employeeService.UpdateEmployeeByIdAsync(employeeModel.CompanyId, employeeModel);

                // Assert
                Assert.NotNull(updateEmployeeResponse);
                Assert.AreEqual(updateEmployeeResponse.Result.Id, employeeModel.CompanyId);
                Assert.AreEqual(updateEmployeeResponse.Result.Name, "test user");
            }

            [Test]
            [Description("Test intended to reproduce the service flow for getting an employee entity")]
            [Author("Aitor Arqué Arnaiz")]
            public void GetEmployeeEntityById()
            {
                // Arrange
                _employeeRepository.Setup(a => a.GetAsync(It.IsAny<string>())).Returns("56, 2000-01-01 00:00:00, 2000-01-01 00:00:00, Email, Fax, Name, 2000-01-01 00:00:00, Password, 1, 2, 3, Telephone, 2000-01-01 00:00:00, Username");
                DateTime time = DateTime.ParseExact("2000-01-01 00:00:00", "yyyy-dd-MM HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                GetEmployeeByIdRequest employeeGetModel = new GetEmployeeByIdRequest()
                {
                    CompanyId = new Random(100).Next(),
                };      

                // Act
                Employee getEmployeeResponse = _employeeService.GetEmployeeByIdAsync(employeeGetModel.CompanyId).Result;

                // Assert
                Assert.NotNull(getEmployeeResponse);
                Assert.AreEqual(getEmployeeResponse.CompanyId, 56);
                Assert.AreEqual(getEmployeeResponse.CreatedOn, Convert.ToDateTime("2000-01-01 00:00:00"));
                Assert.AreEqual(getEmployeeResponse.DeletedOn, Convert.ToDateTime("2000-01-01 00:00:00"));
                Assert.AreEqual(getEmployeeResponse.Email, "Email");
                Assert.AreEqual(getEmployeeResponse.Fax, "Fax");
                Assert.AreEqual(getEmployeeResponse.Name, "Name");
                Assert.AreEqual(getEmployeeResponse.Lastlogin, Convert.ToDateTime("2000-01-01 00:00:00"));
                Assert.AreEqual(getEmployeeResponse.Password, "Password");
                Assert.AreEqual(getEmployeeResponse.PortalId, 1);
                Assert.AreEqual(getEmployeeResponse.RoleId, 2);
                Assert.AreEqual(getEmployeeResponse.StatusId, EmployeeStatus.Vacation);
                Assert.AreEqual(getEmployeeResponse.Telephone, "Telephone");
                Assert.AreEqual(getEmployeeResponse.UpdatedOn, Convert.ToDateTime("2000-01-01 00:00:00"));
                Assert.AreEqual(getEmployeeResponse.Username, "Username");
            }

            [Test]
            [Description("Test intended to reproduce the service flow for getting all existing employees")]
            [Author("Aitor Arqué Arnaiz")]
            public void GetAllEmployees()
            {
            // Arrange
            _employeeRepository.Setup(a => a.ListAsync(It.IsAny<string>())).Returns(new List<string>() { "56, 2000-01-01 00:00:00, 2000-01-01 00:00:00, Email, Fax, Name, 2000-01-01 00:00:00, Password, 1, 2, 3, Telephone, 2000-01-01 00:00:00, Username", 
                "87, 2021-01-01 00:00:00, 2021-01-01 00:00:00, Email2, Fax2, Name2, 2021-01-01 00:00:00, Password2, 4, 5, 6, Telephone, 2021-01-01 00:00:00, Username_2" });

            // Act
            List<Employee> getEmployeesResponse = _employeeService.GetAllEmployeesAsync().Result;

                // Assert
                Assert.NotNull(getEmployeesResponse);
                foreach (Employee e in getEmployeesResponse)
                {
                    Assert.NotNull(e);
                    Assert.IsNotEmpty(e.CompanyId.ToString());
                    Assert.IsNotEmpty(e.CreatedOn.ToString());
                    Assert.IsNotEmpty(e.DeletedOn.ToString());
                    Assert.IsNotEmpty(e.Email);
                    Assert.IsNotEmpty(e.Fax);
                    Assert.IsNotEmpty(e.Name);
                    Assert.IsNotEmpty(e.Lastlogin.ToString());
                    Assert.IsNotEmpty(e.Password);
                    Assert.IsNotEmpty(e.PortalId.ToString());
                    Assert.IsNotEmpty(e.RoleId.ToString());
                    Assert.IsNotEmpty(e.StatusId.ToString());
                    Assert.IsNotEmpty(e.Telephone);
                    Assert.IsNotEmpty(e.UpdatedOn.ToString());
                    Assert.IsNotEmpty(e.Username);
                }
            }

        #endregion
    }
}