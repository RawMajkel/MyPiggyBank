﻿using Microsoft.AspNetCore.Mvc;
using MyPiggyBank.Core.Protocol.Query;
using MyPiggyBank.Core.Service.Implementation;
using MyPiggyBank.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get([FromQuery] OperationCategoriesQuery query)
        {
            var resources = _operationCategoriesService.GetOperationCategories(query);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resources.PagingData()));
            return Ok(resources);
        }

        [HttpPost]
        public void Post([FromBody] OperationCategory opc)
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
        public void Put(Guid id, [FromBody] OperationCategory opc)
            => _operationCategoriesService.SaveOperationCategory(opc);

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
            => _operationCategoriesService.DeleteOperationCategory(id);
    }
}