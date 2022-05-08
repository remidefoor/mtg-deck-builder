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
        [ProducesResponseType(typeof(IEnumerable<CardReadDTO>), 200)]
        public ActionResult<IEnumerable<CardReadDTO>> GetCards([FromQuery] CardFilter cardFilter)
        {
            try
            {
                IEnumerable<CardReadDTO> cards = _cardRepository.ReadCards()
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
