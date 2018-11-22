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
    public class EmployeeController : ControllerBase
    {

        private readonly IOrgEmployee _employeeSvr;

        public EmployeeController(IOrgEmployee employeeSvr)
        {
            _employeeSvr = employeeSvr;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Organization_Employee>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllEmployee() {
            var employeeList = await _employeeSvr.GetEmployeeList();

            if (employeeList is null)
                return Ok();

            return Ok(employeeList);
        }


    }
}