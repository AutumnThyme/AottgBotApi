using AottgBotApi.Data;
using AottgBotApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AottgBotApi.Controllers
{
    // api/aottgcommands
    // [Route("api/[controller]")]
    [Route("api/tutorial")]
    [ApiController]
    public class TutorialCommandsController : ControllerBase
    {
        private readonly ITutorialCommandRepo _repository;

        public TutorialCommandsController(ITutorialCommandRepo repository)
        {
            _repository = repository;
        }

        // Get api/tutorial
        [HttpGet]
        public ActionResult<IEnumerable<TutorialCommand>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(commandItems);
        }

        // GET api/tutorial/{id}
        // Ex: GET api/tutorial/5
        [HttpGet("{id}")]
        public ActionResult<TutorialCommand> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            return Ok(command);
        }

    }
}