using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.Business.Services;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagementV2.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IService<Department, string> service;

        public DepartmentController(IService<Department, string> service, IMapper _mapper)
        {
            this.service = service;
            this._mapper = _mapper;
        }

        // GET: api/<DepartmentController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of DepartmentViewModel (Size >= 0)</returns>
        [HttpGet("get-all-departments")]
        public IEnumerable<DepartmentViewModel> GetAll()
        {
            IEnumerator<Department> departments = service.GetAll().GetEnumerator();

            List<DepartmentViewModel> result = new List<DepartmentViewModel>();

            while (departments.MoveNext())
            {
                result.Add(_mapper.Map<DepartmentViewModel>(departments.Current));
            }

            return result;
        }

        //GET api/<DepartmentController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DepartmentViewModel or Null</returns>
        [HttpGet("get-department/{id}")]
        public DepartmentViewModel GetById(string id)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<DepartmentViewModel>(service.GetById(id));
        }

        // POST api/<DepartmentController>
        [HttpPost("save-department")]
        public void Post([FromBody] DepartmentViewModel departmentViewModel)
        {
            service.Save(_mapper.Map<Department>(departmentViewModel));
            
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("update-department")]
        public void Put([FromBody] DepartmentViewModel departmentViewModel)
        {
            service.Update(_mapper.Map<Department>(departmentViewModel));
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("delete-department/{id}")]
        public void Delete(string id)
        {
            service.Delete(id);
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("validate-department")]
        public JsonResult ValidateDepartment([FromForm] DepartmentViewModel departmentViewModel)
        {
            Department department = service.GetById(departmentViewModel.DeptId);

            if (department != null)
            {

                return new JsonResult(new { isDepartmentExisted = true });

            }

            return new JsonResult(new { isSuccess = true });

        }
    }
}
