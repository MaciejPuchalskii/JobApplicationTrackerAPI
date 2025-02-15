namespace JobApplicationTrackerAPI.Service.NoteService
{
    public class NoteService : INoteService
    {
        private readonly INoteService _noteService;

        public NoteService(INoteService noteService)
        {
            _noteService = noteService;
        }
    }
}