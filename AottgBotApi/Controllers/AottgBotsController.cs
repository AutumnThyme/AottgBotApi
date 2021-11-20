using AottgBotApi.Data;
using AottgBotApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AottgBotApi.Controllers
{
    // api/aottgcommands
    // [Route("api/[controller]")]
    [Route("api/aottg")]
    [ApiController]
    public class AottgBotsController : ControllerBase
    {
        private readonly IAottgBotRepo _repository;

        public AottgBotsController(IAottgBotRepo repository)
        {
            _repository = repository;
        }

        // Get api/tutorial
        [HttpGet("{region}")]
        public ActionResult<IEnumerable<TutorialCommand>> GetServerlist(string region)
        {
            var serverlist = _repository.GetServerList(region);

            return Ok(serverlist);
        }
    }
}
