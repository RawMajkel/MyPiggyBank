using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Service.Implementation;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MyPiggyBank.Web.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OperationCategoriesController : BaseController
    {
        private readonly IOperationCategoriesService _operationCategoriesService;
        public OperationCategoriesController(IOperationCategoriesService operationsService)
        {
            _operationCategoriesService = operationsService;
        }

        [HttpGet]
        public IActionResult Get([FromBody] OperationCategoriesGetRequest query)
            => ReturnBadRequestIfThrowError(() =>
            {
                var resources = _operationCategoriesService.GetOperationCategories(query, UserId);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
                return resources;
            });

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OperationCategoriesSaveRequest opc)
            => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.SaveOperationCategory(opc, UserId));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
         => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.Get(id));

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] OperationCategoriesSaveRequest opc)
            => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.SaveOperationCategory(opc, UserId));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.DeleteOperationCategory(id));
    }
}
