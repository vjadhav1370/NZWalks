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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _db.AddAsync(region);
            await _db.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var result= await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
            {
                return null;
            }
            _db.Regions.Remove(result);
            await _db.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _db.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid Id)
        {
            return await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        
        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            var result = await _db.Regions.FirstOrDefaultAsync(x => x.Id == Id);   
            if(result==null)
            {
                return null;
            }
            result.Code= region.Code;
            result.Name= region.Name;
            result.Area = region.Area;
            result.lat = region.lat;
            result.Long = region.Long;
            result.Population = region.Population;

            await _db.SaveChangesAsync();
            return result;

        }
    }
}
