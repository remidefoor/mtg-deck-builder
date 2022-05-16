using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaritiesController : ControllerBase
    {
        private readonly IRarityRepository _rarityRepository;
        private readonly IMapper _mapper;

        public RaritiesController(IRarityRepository rarityRepository, IMapper mapper)
        {
            _rarityRepository = rarityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RarityReadDTO>), 200)]
        public async Task<ActionResult<IEnumerable<RarityReadDTO>>> GetRarities()
        {
            return (_rarityRepository.ReadRarities() is IQueryable<Rarity> rarities)
                ? Ok(await rarities.ProjectTo<RarityReadDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync())
                : Ok(new List<RarityReadDTO>());
        }
    }
}
