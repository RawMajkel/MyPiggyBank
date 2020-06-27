using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Service;
using MyPiggyBank.Data.Model;
using Newtonsoft.Json;
using MyPiggyBank.Core.Protocol.Resource.Requests;

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
        public IActionResult Get([FromQuery] ResourceGetRequest query)
        {
            var resources = _resourcesService.GetResources(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ResourceSaveRequest res)
        {
            try
            {
                await _resourcesService.SaveResource(res);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _resourcesService.Get(id);
                return Ok(result);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ResourceSaveRequest res)
        {
            try
            {
                await _resourcesService.SaveResource(res);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _resourcesService.DeleteResource(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
