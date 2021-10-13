using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RedArborAPI.Business.Services;
using RedArborAPI.Controllers;
using RedArborAPI.Domain.Entities.Employee;
using RedArborAPI.Infrastructure.Data.Repositories;
using RedArborAPI.Infrastructure.Interfaces;
using System;
using System.Net;
using static RedArborAPI.Domain.Shared.Enums;

namespace RedArborAPI.Test.Integration_Tests
{
    public class ResArborAPIIntegration
    {
        #region Class Variables/Properties

        // Logger
        ILogger<EmployeeController> _logger;

        // Employee repository
        IEmployeeRepository _employeeRepository;

        // Employee service
        EmployeeService _employeeService;

        // Employee controller
        EmployeeController _employeeController;

        #endregion

        #region Class Setup Methods

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new EmployeeRepository();
            _employeeService = new EmployeeService(_employeeRepository);
            _employeeController = new EmployeeController(_logger, _employeeService);
        }

        [TearDown]
        public void TearDown()
        {
            _employeeController = null;
            _employeeRepository = null;
            _employeeService = null;
        }

        #endregion

        #region Class Tests

            [Test]
            [Description("Test intended to reproduce the service flow for adding a new employee entity from the controller. This test attacks against real data base")]
            [Author("Aitor Arqué Arnaiz")]
            public void AddEmployee_Test()
            {
                // Arrange
                Random random = new Random();
                Employee employee = new Employee()
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
                    StatusId = EmployeeStatus.Contracted,
                    Telephone = "569347640",
                    UpdatedOn = new DateTime().ToUniversalTime(),
                    Username = "test user"
                };

                // Act
                ActionResult<Employee> addEmployeeResponse = _employeeController.AddEmployeeController(employee);

                // Assert
                Assert.NotNull(addEmployeeResponse);
            }

            [Test]
            [Description("Test intended to reproduce the service flow for getting an employee entity forcing an error ")]
            [Author("Aitor Arqué Arnaiz")]
            [Ignore("Not working")]
            public void GetEmployeeEntityById_Exception()
            {
                // Arrange

                // Act & Assert
                Assert.AreEqual(HttpStatusCode.NotFound, _employeeController.GetEmployeeByIdController(null));

            }

        #endregion

    }
}
