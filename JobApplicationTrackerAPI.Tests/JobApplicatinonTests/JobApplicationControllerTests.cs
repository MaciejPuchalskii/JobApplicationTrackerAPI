using FluentAssertions;
using JobApplicationTrackerAPI.Controllers;
using JobApplicationTrackerAPI.DTOs.Command.JobApplication;
using JobApplicationTrackerAPI.DTOs.Response.JobApplication;
using JobApplicationTrackerAPI.Service.JobApplicationService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JobApplicationTrackerAPI.Tests.JobApplicatinonTests
{
    public class JobApplicationControllerTests
    {
        private readonly Mock<IJobApplicationService> _serviceMock;
        private readonly JobApplicationController _controller;

        public JobApplicationControllerTests()
        {
            _serviceMock = new Mock<IJobApplicationService>();
            _controller = new JobApplicationController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithJobApplications()
        {
            // Assert
            var jobApplications = new List<JobApplicationGetAllResponseDto>
            {
                new() { Id = Guid.NewGuid(),Position ="Software Developer" },
                new() { Id = Guid.NewGuid(), Position= "Backend Developer" }
            };

            _serviceMock.Setup(service => service.GetAll()).ReturnsAsync(jobApplications);

            //Act

            var result = await _controller.GetAll();

            // Assert

            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(jobApplications);

            _serviceMock.Verify(service => service.GetAll(), Times.Once());
        }

        [Fact]
        public async Task Create_ShouldReturn201Created_WhenJobApplicationIsSuccessfullyAdded()
        {
            // Assert
            var addJobApplicationDto = new AddJobApplicationCommandDto
            {
                Position = "Software Developer",
                Description = "Test",
                JobAdvertURL = "Url",
                CompanyId = Guid.NewGuid(),
                AppliedDate = DateTime.Now,
                Status = Data.Enums.JobApplicationStatus.Applied
            };

            var createdJobApplication = new JobApplicationAddResponseDto
            {
                Id = Guid.NewGuid(),
                Position = "Software Developer",
                AppliedDate = addJobApplicationDto.AppliedDate,
            };

            var userId = Guid.NewGuid().ToString();
            _serviceMock.Setup(service => service.Add(addJobApplicationDto, userId)).ReturnsAsync(createdJobApplication);

            // Act

            var result = await _controller.Create(addJobApplicationDto);

            // Assert

            var createdAtResult = result as CreatedAtActionResult;
            createdAtResult.Should().NotBeNull();
            createdAtResult.StatusCode.Should().Be(201);
            createdAtResult.Value.Should().BeEquivalentTo(createdJobApplication, options => options.Excluding(x => x.Id));

            _serviceMock.Verify(service => service.Add(addJobApplicationDto, userId), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturn200OK_WhenJobApplicationIsSuccessfullyUpdated()
        {
            var updateDto = new UpdateJobApplicationCommandDto
            {
                Id = Guid.NewGuid(),
                Position = "Updated Position",
                Description = "Updated Description",
                JobAdvertURL = "UpdatedUrl",
                CompanyId = Guid.NewGuid(),
                AppliedDate = DateTime.Now,
                Status = Data.Enums.JobApplicationStatus.InterviewScheduled
            };

            var updatedJobApplication = new JobApplicationResponseDto
            {
                Id = updateDto.Id,
                Position = updateDto.Position,
                Description = updateDto.Description,
                JobAdvertURL = updateDto.JobAdvertURL,
                CompanyId = updateDto.CompanyId,
                AppliedDate = updateDto.AppliedDate,
                Status = updateDto.Status
            };

            _serviceMock.Setup(service => service.Update(updateDto))
                        .ReturnsAsync(updatedJobApplication);

            // Act
            var result = await _controller.Update(updateDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(updatedJobApplication);

            _serviceMock.Verify(service => service.Update(updateDto), Times.Once());
        }

        [Fact]
        public async Task Update_ShouldReturn404NotFound_WhenJobApplicationIsNotFound()
        {
            // Arrange
            var updateDto = new UpdateJobApplicationCommandDto
            {
                Id = Guid.NewGuid(),
                Position = "NonExistent Position",
                Description = "NonExistent Description",
                JobAdvertURL = "NonExistentUrl",
                CompanyId = Guid.NewGuid(),
                AppliedDate = DateTime.Now,
                Status = Data.Enums.JobApplicationStatus.Applied
            };

            _serviceMock.Setup(service => service.Update(updateDto))
                        .ReturnsAsync((JobApplicationResponseDto)null);

            // Act
            var result = await _controller.Update(updateDto);

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);

            _serviceMock.Verify(service => service.Update(updateDto), Times.Once());
        }

        [Fact]
        public async Task Delete_ShouldReturn200OK_WhenJobApplicationIsSuccessfulyRemoved()
        {
            // Arrange
            var jobApplicationId = Guid.NewGuid();

            _serviceMock.Setup(service => service.Delete(jobApplicationId))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(jobApplicationId);

            // Assert
            var okResult = result as OkResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            _serviceMock.Verify(service => service.Delete(jobApplicationId), Times.Once());
        }

        [Fact]
        public async Task GetByCompanyId_ShouldReturnOk200WithJobApplications()
        {
            // Arrange
            var companyId = Guid.NewGuid();
            var jobApplications = new List<JobApplicationGetAllResponseDto>
            {
                new() { Id = Guid.NewGuid(), Position = "Software Developer" },
                new() { Id = Guid.NewGuid(), Position = "Backend Developer" }
            };

            _serviceMock.Setup(service => service.GetByCompanyId(companyId))
                        .ReturnsAsync(jobApplications);

            // Act
            var result = await _controller.GetByCompanyId(companyId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(jobApplications);

            _serviceMock.Verify(service => service.GetByCompanyId(companyId), Times.Once());
        }
    }
}