using AottgBotApi.Data;
using AottgBotApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet("serverlist/{region}")]
        [EnableCors()]
        public ActionResult<IEnumerable<TutorialCommand>> GetServerlist(string region)
        {
            try
            {
                var serverlist = _repository.GetServerList(region);

                if (serverlist == null)
                {
                    return StatusCode(400, $"Could not fetch serverlist for region {region}.");
                }

                Console.WriteLine("Requested");

                return Ok(serverlist);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
            
        }

        // Get api/tutorial
        [HttpGet("serverlistold/{region}")]
        [EnableCors()]
        public ActionResult<IEnumerable<TutorialCommand>> GetServerlistOld(string region)
        {
            try
            {
                var serverlist = _repository.GetServerListSingleResource(region);

                if (serverlist == null)
                {
                    return StatusCode(400, $"Could not fetch serverlist for region {region}.");
                }

                return Ok(serverlist);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }

        // Get api/tutorial
        [HttpGet("regions")]
        [EnableCors()]
        public ActionResult<IEnumerable<TutorialCommand>> GetValidRegions()
        {
            var serverlist = _repository.GetValidRegions();

            return Ok(serverlist);
        }
    }
}
