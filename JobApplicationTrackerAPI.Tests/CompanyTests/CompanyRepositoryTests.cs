using FluentAssertions;
using JobApplicationTrackerAPI.Data;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Repository;
using JobApplicationTrackerAPI.Repository.Company;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.Tests.CompanyTests
{
    public class CompanyRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CompanyRepository _repository;

        public CompanyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new ApplicationDbContext(options);
            _repository = new CompanyRepository(_context);
        }

        [Fact]
        public async Task ExistsByName_ShouldReturnTrue_WhenCompanyExists()
        {
            // Arrange
            var company = new Company()
            {
                Name = "I exist!",
                Description = "You don't need to check this!!!"
            };

            await _repository.Add(company);

            // Act
            var result = await _repository.ExistByName(company.Name);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistByName_ShouldReturnFalse_WhenCompanyDoesNotExist()
        {
            // Arrange
            var company = new Company()
            {
                Name = "I exist!",
                Description = "You don't need to check this!!!"
            };

            await _repository.Add(company);

            // Act
            var result = await _repository.ExistByName("Nonexistent Company");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ExistByName_ShouldBeCaseInsensitive()
        {
            // Arrange
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test Company",
            };

            await _repository.Add(company);

            // Act
            var result = await _repository.ExistByName("test company");

            // Assert
            result.Should().BeTrue();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}