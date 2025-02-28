using FluentAssertions;
using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.Models;
using JobApplicationTrackerAPI.Repository.JobApplication;
using JobApplicationTrackerAPI.Service.JobApplicationService;
using Moq;

namespace JobApplicationTrackerAPI.Tests.JobApplicatinonTests
{
    public class JobApplicationServiceTests
    {
        private readonly Mock<IJobApplicationRepository> _jobApplicationRepositoryMock;
        private readonly JobApplicationService _jobApplicationService;

        public JobApplicationServiceTests()
        {
            _jobApplicationRepositoryMock = new Mock<IJobApplicationRepository>();
            _jobApplicationService = new JobApplicationService(_jobApplicationRepositoryMock.Object);
        }

        [Fact]
        public async Task Add_ShouldAddJobApplication_WhenValid()
        {
            // Arrange

            var addJobApplicationDto = new AddJobApplicationCommandDto()
            {
                AppliedDate = new DateTime(2025, 2, 28),
                CompanyId = Guid.NewGuid(),
                JobAdvertURL = "Test",
                Position = "software developer",
                Status = Data.Enums.JobApplicationStatus.Applied
            };

            _jobApplicationRepositoryMock.Setup(repo => repo.Add(It.IsAny<JobApplication>())).Returns(Task.CompletedTask);

            // Act

            var userId = Guid.NewGuid().ToString();
            var result = await _jobApplicationService.Add(addJobApplicationDto, userId);

            // Assert

            result.Should().NotBeNull();
            result.Position.Should().Be(addJobApplicationDto.Position);
            result.AppliedDate.Should().Be(addJobApplicationDto.AppliedDate);
            result.CompanyId.Should().Be(addJobApplicationDto.CompanyId);

            _jobApplicationRepositoryMock.Verify(repo => repo.Add(It.IsAny<JobApplication>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrueAndRemoveJobApplication_WhenJobApplicationIdExists()
        {
            // Arrange

            var jobApplication = new JobApplication()
            {
                Id = Guid.NewGuid(),
                Position = "Junior Fullstack Developer"
            };
            var idToDelete = jobApplication.Id;

            _jobApplicationRepositoryMock.Setup(repo => repo.GetById(idToDelete)).ReturnsAsync(jobApplication);
            _jobApplicationRepositoryMock.Setup(repo => repo.Delete(jobApplication)).Returns(Task.CompletedTask);

            // Act

            var result = await _jobApplicationService.Delete(idToDelete);

            //Assert

            result.Should().BeTrue();
            _jobApplicationRepositoryMock.Verify(repo => repo.GetById(idToDelete), Times.Once);
            _jobApplicationRepositoryMock.Verify(repo => repo.Delete(jobApplication), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenJobApplicationIdDoesNotExist()
        {
            // Arrange

            var nonExisitingId = Guid.NewGuid();

            _jobApplicationRepositoryMock.Setup(repo => repo.GetById(nonExisitingId)).ReturnsAsync((JobApplication)null);

            // Act

            var result = await _jobApplicationService.Delete(nonExisitingId);

            //Assert
            result.Should().BeFalse();
            _jobApplicationRepositoryMock.Verify(repo => repo.GetById(nonExisitingId), Times.Once);
            _jobApplicationRepositoryMock.Verify(repo => repo.Delete(It.IsAny<JobApplication>()), Times.Never);
        }

        [Fact]
        public async Task Update_ShouldModifyJobApplication_WhenJobApplicationIdExists()
        {
            // Arrange

            var existingJobApplication = new JobApplication
            {
                Id = Guid.NewGuid(),
                Position = "Junior Developer",
                Description = "Old Description",
                JobAdvertURL = "OldUrl",
                CompanyId = Guid.NewGuid(),
                AppliedDate = new DateTime(2025, 2, 28),
                Status = Data.Enums.JobApplicationStatus.Applied
            };

            var updateDto = new UpdateJobApplicationCommandDto
            {
                Id = existingJobApplication.Id,
                Position = "Senior Developer",
                Description = "Updated Description",
                JobAdvertURL = "UpdatedUrl",
                CompanyId = existingJobApplication.CompanyId,
                AppliedDate = new DateTime(2025, 3, 1),
                Status = Data.Enums.JobApplicationStatus.InterviewScheduled
            };

            _jobApplicationRepositoryMock.Setup(repo => repo.GetById(updateDto.Id)).ReturnsAsync(existingJobApplication);

            _jobApplicationRepositoryMock.Setup(repo => repo.Update(It.IsAny<JobApplication>())).Returns(Task.CompletedTask);

            // Act
            var result = await _jobApplicationService.Update(updateDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(updateDto.Id);
            result.Position.Should().Be(updateDto.Position);
            result.Description.Should().Be(updateDto.Description);
            result.JobAdvertURL.Should().Be(updateDto.JobAdvertURL);
            result.CompanyId.Should().Be(updateDto.CompanyId);
            result.AppliedDate.Should().Be(updateDto.AppliedDate);
            result.Status.Should().Be(updateDto.Status);

            _jobApplicationRepositoryMock.Verify(repo => repo.GetById(updateDto.Id), Times.Once);
            _jobApplicationRepositoryMock.Verify(repo => repo.Update(It.IsAny<JobApplication>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllJobApplications()
        {
            // Arrange
            var jobApplications = new List<JobApplication>
            {
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Developer",
                    AppliedDate = new DateTime(2025, 2, 28),
                    CompanyId = Guid.NewGuid(),
                    Status = Data.Enums.JobApplicationStatus.Applied
                },
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Tester",
                    AppliedDate = new DateTime(2025, 2, 28),
                    CompanyId = Guid.NewGuid(),
                    Status = Data.Enums.JobApplicationStatus.Applied
                }
            };

            _jobApplicationRepositoryMock.Setup(repo => repo.GetAll())
                                         .ReturnsAsync(jobApplications);

            // Act
            var result = await _jobApplicationService.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(jobApplications.Count);
            foreach (var jobApp in jobApplications)
            {
                result.Should().ContainSingle(r => r.Id == jobApp.Id &&
                                                   r.Position == jobApp.Position &&
                                                   r.AppliedDate == jobApp.AppliedDate &&
                                                   r.CompanyId == jobApp.CompanyId &&
                                                   r.Status == jobApp.Status);
            }

            _jobApplicationRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetByCompanyId_ShouldReturnJobApplications_WhenCompanyIdIsValid()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var jobApplications = new List<JobApplication>
            {
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Developer",
                    AppliedDate = new DateTime(2025, 2, 28),
                    CompanyId = companyId,
                    Status = Data.Enums.JobApplicationStatus.Applied
                },
                new JobApplication
                {
                    Id = Guid.NewGuid(),
                    Position = "Tester",
                    AppliedDate = new DateTime(2025, 2, 28),
                    CompanyId = companyId,
                    Status = Data.Enums.JobApplicationStatus.Applied
                }
            };

            _jobApplicationRepositoryMock.Setup(repo => repo.GetByCompanyId(companyId))
                                         .ReturnsAsync(jobApplications);

            // Act
            var result = await _jobApplicationService.GetByCompanyId(companyId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(jobApplications.Count);
            foreach (var jobApp in jobApplications)
            {
                result.Should().ContainSingle(r => r.Id == jobApp.Id &&
                                                   r.Position == jobApp.Position &&
                                                   r.AppliedDate == jobApp.AppliedDate &&
                                                   r.CompanyId == jobApp.CompanyId &&
                                                   r.Status == jobApp.Status);
            }

            _jobApplicationRepositoryMock.Verify(repo => repo.GetByCompanyId(companyId), Times.Once);
        }

        [Fact]
        public async Task GetByCompanyId_ShouldThrowException_WhenCompanyIdIsInvalid()
        {
            // Arrange
            var invalidCompanyId = Guid.NewGuid();
            _jobApplicationRepositoryMock.Setup(repo => repo.GetByCompanyId(invalidCompanyId))
                                         .ThrowsAsync(new Exception("Invalid company id"));

            // Act
            Func<Task> act = async () => await _jobApplicationService.GetByCompanyId(invalidCompanyId);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid company id");
            _jobApplicationRepositoryMock.Verify(repo => repo.GetByCompanyId(invalidCompanyId), Times.Once);
        }
    }
}