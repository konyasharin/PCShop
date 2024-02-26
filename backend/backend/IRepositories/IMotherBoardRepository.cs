using backend.Entities;

namespace backend.IRepositories
{
    public interface IMotherBoardRepository
    {
        Task<List<MotherBoard>> GetAllMotherBoards();
        Task<MotherBoard> GetMotherBoardById(long id);
        Task AddMotherBoard(MotherBoard motherBoard);
        Task UpdateMotherBoard(MotherBoard motherBoard);
        Task DeleteMotherBoard(long id);
    }
}
