using Howest.MagicCards.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardsController(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CardReadDTO>> GetCards([FromQuery] CardFilter cardFilter)
        {
            return (_cardRepository.ReadCards() is IQueryable<Card> cards)
                ? Ok(cards.Filter(cardFilter)
                    .ProjectTo<CardReadDTO>(_mapper.ConfigurationProvider)
                    .ToList())
                : Ok();
        }
    }
}
