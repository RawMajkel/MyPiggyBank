using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;
using MyPiggyBank.Core.Service;
using Newtonsoft.Json;
using System;
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
        public IActionResult Get([FromBody] CyclicOperationGetRequest query)
        {
            var resources = _cyclicOperationsService.GetCyclicOperations(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] CyclicOperationSaveRequest cop)
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
        public void Put([FromBody] CyclicOperationSaveRequest cop)
            => _cyclicOperationsService.SaveCyclicOperation(cop);

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _cyclicOperationsService.DeleteCyclicOperation(id);
    }
}
