using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hexagon.Entity;
using Hexagon.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseModuleController : ControllerBase
    {

        private readonly IBaseModuleService _moduleSvr;

        public BaseModuleController(IBaseModuleService moduleSvr)
        {
            _moduleSvr = moduleSvr;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Base_Module>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllModule()
        {
            var employeeList = await _moduleSvr.GetModuleList();

            if (employeeList is null)
                return Ok();

            return Ok(employeeList);
        }

    }
}