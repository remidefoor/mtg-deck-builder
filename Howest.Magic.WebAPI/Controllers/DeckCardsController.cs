using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/Decks/{deckId:long}/[controller]")]
    [ApiController]
    public class DeckCardsController : ControllerBase
    {
        private readonly IDeckCardRepository _deckCardRepository;
        private readonly IMapper _mapper;

        public DeckCardsController(IDeckCardRepository deckCardRepository, IMapper mapper)
        {
            _deckCardRepository = deckCardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeckCardReadDetailDTO>), 200)]
        public async Task<ActionResult<IEnumerable<DeckCardReadDetailDTO>>> GetDeckCards(long deckId)
        {
            return (_deckCardRepository.ReadDeckCards(deckId) is IQueryable<DeckCard> deckCards)
                ? Ok(await deckCards.ProjectTo<DeckCardReadDetailDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync())
                : Ok(new List<DeckCardReadDetailDTO>());
        }
    }
}
