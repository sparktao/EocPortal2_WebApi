using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hexagon.Entity;
using Hexagon.IService;
using Microsoft.AspNetCore.Authorization;
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
        // Get api/employee
        [HttpGet(Name ="Get")]
        public async Task<IActionResult> Get() {
            var employeeList = await _employeeSvr.GetEmployeeList();

            if (employeeList is null)
                return Ok();

            var data = new {
                totalCount = employeeList.Count,
                items = employeeList               
            };

            return Ok(data);
        }


    }
}