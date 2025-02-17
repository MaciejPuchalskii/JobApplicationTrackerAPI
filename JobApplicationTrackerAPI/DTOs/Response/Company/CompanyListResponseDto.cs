namespace JobApplicationTrackerAPI.DTOs.Response.Company
{
    public class CompanyListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CompanyURL { get; set; }
    }
}