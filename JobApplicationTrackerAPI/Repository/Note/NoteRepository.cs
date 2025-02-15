using JobApplicationTrackerAPI.Data;

namespace JobApplicationTrackerAPI.Repository.Note
{
    public class NoteRepository : GenericRepository<Models.Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}