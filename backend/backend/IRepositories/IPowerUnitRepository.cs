using backend.Entities;

namespace backend.IRepositories
{
    public interface IPowerUnitRepository
    {
        Task<List<PowerUnit>> GetAllPowerUnits();
        Task<PowerUnit> GetPowerUnitById(long id);
        Task AddPowerUnit(PowerUnit powerUnit);
        Task UpdatePowerUnit(PowerUnit powerUnit);
        Task DeletePowerUnit(long id);
    }
}
