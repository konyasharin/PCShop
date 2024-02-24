using backend.Data;
using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.IRepositories;

namespace backend.Repositories
{
    public class VideoCardRepository : IVideoCardRepository
    {
        private readonly DataContext _context;

        public VideoCardRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<VideoCard>> GetAllVideoCards()
        {
            return await _context.VideoCards.ToListAsync();
        }

        public async Task<VideoCard> GetVideoCardById(long id)
        {
            return await _context.VideoCards.FindAsync(id);
        }

        public async Task AddVideoCard(VideoCard videoCard)
        {
            _context.VideoCards.Add(videoCard);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVideoCard(VideoCard videoCard)
        {
            _context.Entry(videoCard).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideoCard(long id)
        {
            var videoCard = await _context.VideoCards.FindAsync(id);
            if (videoCard != null)
            {
                _context.VideoCards.Remove(videoCard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
