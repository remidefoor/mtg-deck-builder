using Howest.MagicCards.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CardsController(ICardRepository cardRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _cache = memoryCache;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CardReadDTO>), 200)]
        public ActionResult<IEnumerable<CardReadDTO>> GetCards([FromQuery] CardFilter cardFilter, [FromQuery] SortFilter sortingFilter)
        {
            try
            {
                IEnumerable<CardReadDTO> cards = _cardRepository.ReadCards()
                    .Sort(sortingFilter) // first 150 of sorted cards, not 150 sorted cards
                    .Filter(cardFilter)
                    .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                    .ToList();
                return Ok(cards);
            } catch (Exception ex)
            {
                return Ok(new List<CardReadDTO>());
            }
        }
    }
}
