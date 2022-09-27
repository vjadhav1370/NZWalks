using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions =await _regionRepository.GetAll();

            var regionsDTO= _mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid Id)
        {
            var region= await _regionRepository.GetAsync(Id);

            if (region == null)
            {
                return NotFound();
            }
           
                var regionDTO = _mapper.Map<Models.DTO.Region>(region);

                return Ok(regionDTO);
            
        }
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid Id)
        {
            //Get region from the database
            var region = await _regionRepository.DeleteAsync(Id);
            //If null NotFound
            if(region==null)
            {
                return NotFound();
            }
            //Convert Response DTO
            var regionsDTO = _mapper.Map<Models.DTO.Region>(region);
            //Return Ok response
            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to domain model
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                lat = addRegionRequest.lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //Pass detail to repository
            region=await _regionRepository.AddAsync(region);

            //convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                lat = region.lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { Id = regionDTO.Id }, regionDTO);

        }
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid Id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest) 
        {
            //convert DTO Domain model
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                lat = updateRegionRequest.lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            //Update Region using repository
            await _regionRepository.UpdateAsync(Id, region);
            //If null
            if(region==null)
            {
                return NotFound();
            }
            //Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                
                Code = region.Code,
                Area = region.Area,
                lat = region.lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //return Ok
            return Ok(regionDTO);
        }
    }
}
