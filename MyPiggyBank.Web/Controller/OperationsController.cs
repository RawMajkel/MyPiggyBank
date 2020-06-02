using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data.Model;
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

        [HttpGet]
        public IActionResult Get([FromQuery] OperationsQuery query) {
            var resources = _operationsService.GetOperations(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] Operation op)
            => _operationsService.SaveOperation(op);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _operationsService.Get(id));

        [HttpPut("{id:guid}")]
        public void Put(Guid id, [FromBody] Operation op)
            => _operationsService.SaveOperation(op);

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _operationsService.DeleteOperation(id);

    }
}
