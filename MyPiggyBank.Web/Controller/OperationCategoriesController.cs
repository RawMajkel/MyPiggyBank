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
    public class OperationCategoriesController : ControllerBase
    {
        private readonly IOperationCategoriesService _operationCategoriesService;
        public OperationCategoriesController(IOperationCategoriesService operationsService)
        {
            _operationCategoriesService = operationsService;
        }

        [HttpGet]
        public IActionResult Get([FromBody] OperationCategoriesGetRequest query)
        {
            var resources = _operationCategoriesService.GetOperationCategories(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] OperationCategoriesSaveRequest opc)
            => _operationCategoriesService.SaveOperationCategory(opc);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                await _operationCategoriesService.Get(id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public void Put(Guid id, [FromBody] OperationCategoriesSaveRequest opc)
            => _operationCategoriesService.SaveOperationCategory(opc);

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _operationCategoriesService.DeleteOperationCategory(id);
    }
}
