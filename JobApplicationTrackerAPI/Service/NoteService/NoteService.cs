using JobApplicationTrackerAPI.Repository.Note;

namespace JobApplicationTrackerAPI.Service.NoteService
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
    }
}