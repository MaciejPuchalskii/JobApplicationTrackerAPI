using FluentAssertions;
using JobApplicationTrackerAPI.Data;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Repository.JobApplication;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.Tests.JobApplicatinonTests
{
    public class JobApplicationRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly JobApplicationRepository _repository;

        public JobApplicationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new ApplicationDbContext(options);
            _repository = new JobApplicationRepository(_context);
        }

        [Fact]
        public async Task GetByCompanyId_ShouldReturnJobApplications_ForGivenCompanyId()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var otherCompanyId = Guid.NewGuid();

            var jobApplications = new List<JobApplication>
            {
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Developer",
                    CompanyId = companyId,
                    AppliedDate = DateTime.Now,
                    Status = Data.Enums.JobApplicationStatus.Applied
                },
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Tester",
                    CompanyId = companyId,
                    AppliedDate = DateTime.Now,
                    Status = Data.Enums.JobApplicationStatus.Applied
                },
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Manager",
                    CompanyId = otherCompanyId,
                    AppliedDate = DateTime.Now,
                    Status = Data.Enums.JobApplicationStatus.Applied
                }
            };

            await _context.JobApplications.AddRangeAsync(jobApplications);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByCompanyId(companyId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Select(x => x.CompanyId).Distinct().Should().Contain(companyId);
            result.Should().NotContain(x => x.CompanyId == otherCompanyId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}