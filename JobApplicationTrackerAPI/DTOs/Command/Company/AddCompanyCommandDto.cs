namespace JobApplicationTrackerAPI.DTOs.Command.Company
{
    public class AddCompanyCommandDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CompanyURL { get; set; }
    }
}