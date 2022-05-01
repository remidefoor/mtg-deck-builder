﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCards()
        {
            return new LinkedList<Card>();
        }
    }
}
