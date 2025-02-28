using FluentAssertions;
using JobApplicationTrackerAPI.Controllers;
using JobApplicationTrackerAPI.DTOs.Command.Company;
using JobApplicationTrackerAPI.DTOs.Response.Company;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Service.Company;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobApplicationTrackerAPI.Tests.CompanyTests
{
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _controller = new CompanyController(_companyServiceMock.Object);
        }

        [Fact]
        public async Task GetAllCompanies_ShouldReturnOkWithCompanies()
        {
            //Arrange
            var companyDtos = new List<CompanyListResponseDto>
            {
                new CompanyListResponseDto { Id = Guid.NewGuid(), Name = "Company A", Description = "Desc A" },
                new CompanyListResponseDto { Id = Guid.NewGuid(), Name = "Company B", Description = "Desc B" }
            };

            _companyServiceMock.Setup(service => service.GetAll()).ReturnsAsync(companyDtos);

            //Act
            var result = await _controller.GetAll();

            //Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(companyDtos);

            _companyServiceMock.Verify(service => service.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnCompany_WhenExists()
        {
            //Arrange

            var companyResponseDto = new CompanyResponseDto()
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };

            _companyServiceMock.Setup(s => s.GetById(companyResponseDto.Id)).ReturnsAsync(companyResponseDto);

            //Act
            var result = await _controller.GetById(companyResponseDto.Id);

            //Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(companyResponseDto);

            _companyServiceMock.Verify(service => service.GetById(companyResponseDto.Id), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldCreateCompany_WhenModelValid()
        {
            // Arrange

            var addCompanyDto = new AddCompanyCommandDto()
            {
                Name = "Test",
                Description = "Test",
                ContactPerson = "BIll Gates"
            };

            var createdCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                ContactPerson = "Bill Gates"
            };

            _companyServiceMock.Setup(s => s.Add(addCompanyDto)).ReturnsAsync(createdCompany);

            // Act
            var result = await _controller.Create(addCompanyDto);

            // Assert
            var createdAtResult = result as CreatedAtActionResult;
            createdAtResult.Should().NotBeNull();
            createdAtResult.StatusCode.Should().Be(201);
            createdAtResult.Value.Should().BeEquivalentTo(createdCompany);

            _companyServiceMock.Verify(service => service.Add(addCompanyDto), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnConflict_WhenCompanyNameIsTaken()
        {
            // Arrange
            var addCompanyDto = new AddCompanyCommandDto { Name = "Existing Company" };

            _companyServiceMock.Setup(c => c.ExistByName(addCompanyDto.Name)).ReturnsAsync(true);

            var controller = new CompanyController(_companyServiceMock.Object);

            // Act
            var result = await controller.Create(addCompanyDto);

            // Assert
            var conflictResult = result as ConflictObjectResult;
            conflictResult.Should().NotBeNull();
            conflictResult.StatusCode.Should().Be(409);
            conflictResult.Value.Should().Be("Company with this name already exists.");

            _companyServiceMock.Verify(service => service.ExistByName(addCompanyDto.Name), Times.Once);
            _companyServiceMock.Verify(service => service.Add(It.IsAny<AddCompanyCommandDto>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenIdIsIncorrect()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateCompanyDto = new UpdateCompanyCommandDto { Description = "Updated description" };

            _companyServiceMock.Setup(service => service.Update(id, updateCompanyDto)).ReturnsAsync((Company)null);

            // Act
            var result = await _controller.Update(id, updateCompanyDto);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);

            _companyServiceMock.Verify(service => service.Update(id, updateCompanyDto), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldModifyExistingCompany_WhenIdIsCorrect()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateCompanyDto = new UpdateCompanyCommandDto { Description = "Updated description" };

            var updatedCompany = new Company
            {
                Id = id,
                Name = "Updated Name",
                Description = "Updated description"
            };

            _companyServiceMock.Setup(service => service.Update(id, updateCompanyDto)).ReturnsAsync(updatedCompany);

            // Act
            var result = await _controller.Update(id, updateCompanyDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(updatedCompany);

            _companyServiceMock.Verify(service => service.Update(id, updateCompanyDto), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenCompanyIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();

            _companyServiceMock.Setup(service => service.Delete(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { message = "Successfully removed the company" });

            _companyServiceMock.Verify(service => service.Delete(id), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenFailed()
        {
            // Arrange
            var id = Guid.NewGuid();

            _companyServiceMock.Setup(service => service.Delete(id)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);

            _companyServiceMock.Verify(service => service.Delete(id), Times.Once);
        }
    }
}