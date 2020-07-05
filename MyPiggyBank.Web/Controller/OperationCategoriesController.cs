using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using MyPiggyBank.Core.Service.Implementation;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyPiggyBank.Web.Controller
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OperationCategoriesController : BaseController
    {
        private readonly IOperationCategoriesService _operationCategoriesService;
        public OperationCategoriesController(IOperationCategoriesService operationsService)
        {
            _operationCategoriesService = operationsService;
        }

        [HttpPost("List")]
        public IActionResult FilteredList([FromBody] OperationCategoriesGetRequest query)
            => ReturnBadRequestIfThrowError(() =>
            {
                var resources = _operationCategoriesService.GetOperationCategories(query, UserId);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
                return resources;
            });

        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] OperationCategoriesSaveRequest opc)
            => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.SaveOperationCategory(opc, UserId));

        [HttpGet()]
        public async Task<IActionResult> Get()
         => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.Get(Guid.NewGuid()));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await ReturnBadRequestIfThrowError(async() => await _operationCategoriesService.DeleteOperationCategory(id));
    }
}
