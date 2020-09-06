using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Web.Controller;
using Newtonsoft.Json;

namespace MyPiggyBank.Web.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OperationsController : BaseController
    {
        private readonly IOperationsService _operationsService;
        public OperationsController(IOperationsService operationsService)
        {
            _operationsService = operationsService;
        }

        [HttpGet("List")]
        public IActionResult Get([FromQuery] OperationGetRequest query)
            => ReturnBadRequestIfThrowError(() =>
            {
                var resources = _operationsService.GetOperations(query, UserId);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
                return resources;
            });

        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] OperationSaveRequest op)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.SaveOperation(op));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.Get(id));

        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] OperationSaveRequest op)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.SaveOperation(op));

        [HttpDelete("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.DeleteOperation(id));
    }
}
