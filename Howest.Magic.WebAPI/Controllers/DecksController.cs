using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IMapper _mapper;

        public DecksController(IDeckRepository deckRepository, IMapper mapper)
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeckReadDetailDTO>), 200)]
        public async Task<ActionResult<IEnumerable<DeckReadDetailDTO>>> GetDecks()
        {
            return (_deckRepository.ReadDecks() is IQueryable<Deck> decks)
                ? Ok(await decks.ProjectTo<DeckReadDetailDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync())
                : Ok(new List<DeckReadDetailDTO>());
        }
    }
}
