using JobApplicationTrackerAPI.Repository;
using JobApplicationTrackerAPI.Repository.Attachment;
using JobApplicationTrackerAPI.Repository.Company;
using JobApplicationTrackerAPI.Repository.JobApplication;
using JobApplicationTrackerAPI.Repository.Note;
using JobApplicationTrackerAPI.Service.Attachment;
using JobApplicationTrackerAPI.Service.CompanyService;
using JobApplicationTrackerAPI.Service.JobApplicationService;
using JobApplicationTrackerAPI.Service.NoteService;

namespace JobApplicationTrackerAPI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IAttachmentService, AttachmentService>();

            return services;
        }
    }
}