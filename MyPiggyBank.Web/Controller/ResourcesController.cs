using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Service;
using Newtonsoft.Json;
using MyPiggyBank.Core.Protocol.Resource.Requests;
using MyPiggyBank.Web.Controller;

namespace MyPiggyBank.Web.Controllers 
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResourcesController : BaseController
    {
        private readonly IResourcesService _resourcesService;
        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        [HttpGet("List")]
        public IActionResult Get([FromQuery] ResourceGetRequest query)
            => ReturnBadRequestIfThrowError(() =>
            {
                var resources = _resourcesService.GetResources(query, UserId);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
                return resources;
            });

        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] ResourceSaveRequest res)
            => await ReturnBadRequestIfThrowError(async () => await _resourcesService.SaveResource(res, UserId));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _resourcesService.Get(id));

        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] ResourceSaveRequest res)
            => await ReturnBadRequestIfThrowError(async () => await _resourcesService.SaveResource(res, UserId));


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
            => await ReturnBadRequestIfThrowError(async () => await _resourcesService.DeleteResource(id));
    }
}
