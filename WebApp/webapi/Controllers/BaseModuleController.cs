using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hexagon.Data.Entity;
using Hexagon.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseModuleController : ControllerBase
    {

        private readonly IBaseModuleService _moduleSvr;
        private readonly ILogger<BaseModuleController> logger;

        public BaseModuleController(IBaseModuleService moduleSvr, 
            ILogger<BaseModuleController> logger)
        {
            _moduleSvr = moduleSvr;
            this.logger = logger;
        }

        //Get api/basemodule
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<Base_Module>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var employeeList = await _moduleSvr.GetModuleList();

            logger.LogInformation("===========================================Get All Module=========================");

            if (employeeList is null)
                return Ok();

            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var employee = await _moduleSvr.GetModuleById(id);
            if (employee == null)
                return NotFound();


            return Ok(employee);
        }

    }
}