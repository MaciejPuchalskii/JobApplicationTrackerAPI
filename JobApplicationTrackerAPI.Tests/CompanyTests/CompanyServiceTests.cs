using FluentAssertions;
using JobApplicationTrackerAPI.DTOs.Command.Company;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Repository.Company;
using JobApplicationTrackerAPI.Service.Company;
using Moq;

namespace JobApplicationTrackerAPI.Tests.CompanyTests
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _companyService = new CompanyService(_companyRepositoryMock.Object);
        }

        [Fact]
        public async Task Delete_ShouldRemoveCompany_WhenIdExist()
        {
            //Arrange
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };

            var companyIdToDelete = company.Id;

            _companyRepositoryMock.Setup(repo => repo.GetById(companyIdToDelete)).ReturnsAsync(company);
            _companyRepositoryMock.Setup(repo => repo.Delete(company)).Returns(Task.CompletedTask);

            //Act
            var result = await _companyService.Delete(companyIdToDelete);

            //Assert
            result.Should().BeTrue();
            _companyRepositoryMock.Verify(repo => repo.GetById(companyIdToDelete), Times.Once);
            _companyRepositoryMock.Verify(repo => repo.Delete(company), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenIdNotFound()
        {
            //Arrange
            var companyId = Guid.NewGuid();

            _companyRepositoryMock.Setup(repo => repo.GetById(companyId)).ReturnsAsync((Company)null);

            //Act
            var result = await _companyService.Delete(companyId);

            //Assert
            result.Should().BeFalse();
            _companyRepositoryMock.Verify(repo => repo.GetById(companyId), Times.Once);
            _companyRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Company>()), Times.Never);
        }

        [Fact]
        public async Task Add_ShouldAddCompany_WhenValid()
        {
            //Arrange
            var companyDto = new AddCompanyCommandDto()
            {
                Name = "Test",
            };

            var expectedCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Name = companyDto.Name,
            };

            _companyRepositoryMock.Setup(repo => repo.Add(It.IsAny<Company>())).Returns(Task.CompletedTask);

            //Act

            var result = await _companyService.Add(companyDto);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(companyDto.Name);

            _companyRepositoryMock.Verify(repo => repo.Add(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task ExistByName_ShouldReturnTrue_WhenNameExist()
        {
            //Arrange

            var companyName = "Test name";

            _companyRepositoryMock.Setup(repo => repo.ExistByName(companyName)).ReturnsAsync(true);

            //Act

            var result = await _companyService.ExistByName(companyName);

            //Assert

            result.Should().BeTrue();
            _companyRepositoryMock.Verify(repo => repo.ExistByName(companyName), Times.Once);
        }

        [Fact]
        public async Task ExistByName_ShouldReturnFalse_WhenNameDontExist()
        {
            //Arrange

            var companyName = "Name not found";

            _companyRepositoryMock.Setup(repo => repo.ExistByName(companyName)).ReturnsAsync(false);

            //Act

            var result = await _companyService.ExistByName(companyName);

            //Assert

            result.Should().BeFalse();
            _companyRepositoryMock.Verify(repo => repo.ExistByName(companyName), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllCompanies()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { Id = Guid.NewGuid(), Name = "Company A" },
                new Company { Id = Guid.NewGuid(), Name = "Company B" }
            };

            _companyRepositoryMock
                .Setup(repo => repo.GetAll())
                .ReturnsAsync(companies);

            var service = new CompanyService(_companyRepositoryMock.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(c => c.Name == "Company A");
            result.Should().Contain(c => c.Name == "Company B");

            _companyRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldModifyCompany_WhenIdExist()
        {
            // Arrange

            var companyId = Guid.NewGuid();
            var existingCompany = new Company
            {
                Id = companyId,
                Name = "Old Name",
                Description = "Old Description"
            };

            var updateDto = new UpdateCompanyCommandDto
            {
                Description = "New Description"
            };

            _companyRepositoryMock.Setup(repo => repo.GetById(companyId)).ReturnsAsync(existingCompany);

            _companyRepositoryMock.Setup(repo => repo.Update(It.IsAny<Company>())).Returns(Task.CompletedTask);

            var service = new CompanyService(_companyRepositoryMock.Object);

            // Act

            var result = await service.Update(companyId, updateDto);

            // Assert

            result.Should().NotBeNull();
            result.Description.Should().Be("New Description");

            _companyRepositoryMock.Verify(repo => repo.GetById(companyId), Times.Once);
            _companyRepositoryMock.Verify(repo => repo.Update(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenIdNotFound()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var updateDto = new UpdateCompanyCommandDto { Description = "New Description" };

            _companyRepositoryMock.Setup(repo => repo.GetById(companyId)).ReturnsAsync((Company)null);

            var service = new CompanyService(_companyRepositoryMock.Object);

            // Act
            var result = await service.Update(companyId, updateDto);

            // Assert
            result.Should().BeNull();
            _companyRepositoryMock.Verify(repo => repo.GetById(companyId), Times.Once);
            _companyRepositoryMock.Verify(repo => repo.Update(It.IsAny<Company>()), Times.Never);
        }
    }
}