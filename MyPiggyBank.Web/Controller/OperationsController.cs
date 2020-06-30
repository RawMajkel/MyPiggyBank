using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Web.Controller;
using Newtonsoft.Json;

namespace MyPiggyBank.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OperationsController : BaseController
    {
        private readonly IOperationsService _operationsService;
        public OperationsController(IOperationsService operationsService)
        {
            _operationsService = operationsService;
        }

        [HttpGet]
        public IActionResult Get([FromBody] OperationGetRequest query)
            => ReturnBadRequestIfThrowError(() =>
            {
                var resources = _operationsService.GetOperations(query);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
                return resources;
            });

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OperationSaveRequest op)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.SaveOperation(op));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.Get(id));

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromBody] OperationSaveRequest op)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.SaveOperation(op));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _operationsService.DeleteOperation(id));
    }
}
