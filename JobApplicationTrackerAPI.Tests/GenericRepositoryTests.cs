using FluentAssertions;
using JobApplicationTrackerAPI.Data;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.Tests
{
    public class GenericRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly GenericRepository<Company> _repository;

        public GenericRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new ApplicationDbContext(options);
            _repository = new GenericRepository<Company>(_context);
        }

        [Fact]
        public async Task Add_ShouldAddEntity()
        {
            // Arrange
            var comapny = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test",
                ContactPerson = "Tester Test",
                PhoneNumber = "+48 788 111 222",
                CompanyURL = "Test",
                JobApplications = new List<JobApplication> { }
            };

            // Act
            await _repository.Add(comapny);

            // Assert
            var result = _context.Companies.FirstOrDefault(c => c.Id == comapny.Id);
            result.Should().NotBeNull();
            result.Name.Should().Be(comapny.Name);
            result.Description.Should().Be(comapny.Description);
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Add_ShouldThrowException_WhenEntityIsNull()
        {
            // Arrange
            Company company = null;

            // Act
            Func<Task> act = async () => await _repository.Add(company);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Add_ShouldAssignGuidId()
        {
            // Arrange
            var company = new Company { Name = "New Company" };

            // Act
            await _repository.Add(company);

            // Assert
            company.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            // Arrange
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                Name = "Google",
                Description = "It!"
            };

            // Act
            await _repository.Add(company);
            var result = await _repository.GetById(company.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(company.Name);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEntities()
        {
            // Arrange
            var companies = new List<Company>
            {
                new Company { Id = Guid.NewGuid(), Name = "Company 1" },
                new Company { Id = Guid.NewGuid(), Name = "Company 2" }
            };

            foreach (var company in companies)
            {
                await _repository.Add(company);
            }
            // Act
            var result = await _repository.GetAll();

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Update_ShouldModifyEntity()
        {
            // Arrange
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Old Name",
                Description = "Old Description",
            };
            await _repository.Add(company);

            // Act
            company.Name = "New Name";
            await _repository.Update(company);

            var updatedCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);

            // Assert
            updatedCompany.Should().NotBeNull();
            updatedCompany.Name.Should().Be("New Name");
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Company to Delete",
            };

            await _repository.Add(company);

            // Act
            await _repository.Delete(company);
            var deletedCompany = await _context.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);

            // Assert
            deletedCompany.Should().BeNull();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}