using Howest.MagicCards.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<CardReadDTO>>> GetCards([FromQuery] CardFilter cardFilter, [FromQuery] SortFilter sortFilter)
        {
            if (!cardFilter.HasFilters() && !sortFilter.HasOrder())
            {
                return Ok(await GetCachedCards());
            }

            return Ok(await GetFilteredCards(cardFilter, sortFilter));
        }

        private async Task<IEnumerable<CardReadDTO>> GetCachedCards()
        {
            if (!_cache.TryGetValue("defaultCardSet", out IEnumerable<Card> defaultCardSet))
            {
                int defaultCardAmount = int.Parse(Configuration.GetAppSetting("DefaultCardAmount"));
                defaultCardSet = await _cardRepository.ReadCards()
                    .Take(defaultCardAmount)
                    .ToListAsync();
                _cache.Set("defaultCardSet", defaultCardSet);
            }
            return _mapper.Map<IEnumerable<CardReadDTO>>(defaultCardSet);
        }

        private async Task<IEnumerable<CardReadDTO>> GetFilteredCards(CardFilter cardFilter, SortFilter sortFilter)
        {
            return (_cardRepository.ReadCards() is IQueryable<Card> cards)
                ? await cards.Sort(sortFilter) // first 150 of sorted cards, not 150 sorted cards
                    .Filter(cardFilter)
                    .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync()
                : new List<CardReadDTO>();
        }
    }
}
