using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPiggyBank.Web.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CyclicOperationsController : ControllerBase
    {
        private readonly ICyclicOperationsService _cyclicOperationsService;
        public CyclicOperationsController(ICyclicOperationsService cyclicOperationsService)
        {
            _cyclicOperationsService = cyclicOperationsService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] CyclicOperationsQuery query)
        {
            var resources = _cyclicOperationsService.GetCyclicOperations(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] CyclicOperation cop)
            => _cyclicOperationsService.SaveCyclicOperation(cop);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                await _cyclicOperationsService.Get(id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public void Put(Guid id, [FromBody] CyclicOperation cop)
            => _cyclicOperationsService.SaveCyclicOperation(cop);

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _cyclicOperationsService.DeleteCyclicOperation(id);
    }
}
