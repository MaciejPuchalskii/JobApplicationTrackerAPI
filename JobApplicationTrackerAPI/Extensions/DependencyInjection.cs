using JobApplicationTrackerAPI.Repository;
using JobApplicationTrackerAPI.Repository.AttachmentRepository;
using JobApplicationTrackerAPI.Repository.Company;
using JobApplicationTrackerAPI.Repository.JobApplication;
using JobApplicationTrackerAPI.Repository.Note;
using JobApplicationTrackerAPI.Service.AttachmentService;
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
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<INoteService, NoteService>();

            return services;
        }
    }
}