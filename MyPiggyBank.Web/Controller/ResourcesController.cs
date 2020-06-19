using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Resource;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data.Model;
using Newtonsoft.Json;

namespace MyPiggyBank.Web.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService _resourcesService;
        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ResourcesQuery query)
        {
            var resources = _resourcesService.GetResources(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] Resource res)
            => _resourcesService.SaveResource(res);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                await _resourcesService.Get(id);
                return Ok();
            } catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public void Put(Guid id, [FromBody] Resource res)
            => _resourcesService.SaveResource(res);


        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _resourcesService.DeleteResource(id);
    }
}
