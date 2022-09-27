﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAll();
       Task<Region> GetAsync(Guid Id);
       Task<Region> AddAsync(Region region);
       Task<Region> DeleteAsync(Guid Id);
        Task<Region> UpdateAsync(Guid Id, Region region);
    }
}
