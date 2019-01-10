using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hexagon.Entity;
using Hexagon.IService;
using Hexagon.Util.WebControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using webapi.Models;

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
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader] Pagination pagination) {
            //var employeeList = await _employeeSvr.GetEmployeeList();
            var employeeList = await _employeeSvr.GetPagedEmployeeList(pagination);
            if (employeeList is null)
                return Ok();

            //var data = new {
            //    totalCount = employeeList,
            //    items = employeeList               
            //};

            //return Ok(data);

            var meta = new
            {
                employeeList.TotalItemsCount,
                employeeList.PageSize,
                employeeList.PageIndex,
                employeeList.PageCount
                //previousPageLink,
                //nextPageLink
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
            return Ok(employeeList);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var employee = await _employeeSvr.GetEmployeeById(id);
            return Ok(employee);
        }

        [AllowAnonymous]
        //Post api/employee
        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> Create(CreateOrgEmployeeDTO createEmployee) {

            Organization_Employee employee = new Organization_Employee() {
                Employee_Name = createEmployee.employee_Name,
                Gender = createEmployee.gender,
                Birthday = createEmployee.birthday,
                Contact_Phone = createEmployee.contact_Phone,
                Email = createEmployee.email,
                Isvalid = (createEmployee.isvalid)? 1 : 0                
            };

            int result = await _employeeSvr.InsertEmployee(employee);
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Organization_Employee postEmployee)
        {
            if (postEmployee == null)
            {
                return BadRequest();
            }

            var result = await _employeeSvr.UpdateEmployee(postEmployee);

            if (result > -1)
                return Ok();

            return NoContent();
        }


    }
}