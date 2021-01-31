namespace QuizMania.WebAPI.Data
{
    public abstract class BaseRepository
    {
        protected readonly DatabaseContext _context;
        
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }
    }
}
