using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using MyPiggyBank.Core.Service;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Web.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CyclicOperationsController : BaseController
    {
        private readonly ICyclicOperationsService _cyclicOperationsService;
        public CyclicOperationsController(ICyclicOperationsService cyclicOperationsService)
        {
            _cyclicOperationsService = cyclicOperationsService;
        }

        [HttpGet]
        public IActionResult Get([FromBody] CyclicOperationGetRequest query)
            => ReturnBadRequestIfThrowError(() =>
            {
                var resources = _cyclicOperationsService.GetCyclicOperations(query, UserId);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
                return resources;
            });

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CyclicOperationSaveRequest cop)
            => await ReturnBadRequestIfThrowError(async () => await _cyclicOperationsService.SaveCyclicOperation(cop));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _cyclicOperationsService.Get(id));

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromBody] CyclicOperationSaveRequest cop)
            => await ReturnBadRequestIfThrowError(async () => await _cyclicOperationsService.SaveCyclicOperation(cop));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _cyclicOperationsService.DeleteCyclicOperation(id));
    }
}
