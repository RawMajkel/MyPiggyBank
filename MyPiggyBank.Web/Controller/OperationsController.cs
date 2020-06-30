using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Protocol.Operation.Responses;
using MyPiggyBank.Core.Service;
using Newtonsoft.Json;

namespace MyPiggyBank.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationsService _operationsService;
        public OperationsController(IOperationsService operationsService)
        {
            _operationsService = operationsService;
        }

        [Authorize]
        [HttpGet("Get")]
        public IActionResult Get([FromBody] OperationGetRequest query)
        {
            var resources = _operationsService.GetOperations(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] OperationSaveRequest op)
            => _operationsService.SaveOperation(op);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                await _operationsService.Get(id);
                return Ok();
            } catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public void Put([FromBody] OperationSaveRequest op)
            => _operationsService.SaveOperation(op);

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _operationsService.DeleteOperation(id);
    }
}
