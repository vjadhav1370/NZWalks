using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _db;
        public RegionRepository(NZWalksDbContext db)
        {
            _db= db;
        }
        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _db.Regions.ToListAsync();
        }
    }
}
