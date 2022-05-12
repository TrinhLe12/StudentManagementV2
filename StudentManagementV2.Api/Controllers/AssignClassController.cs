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
    public class AssignClassController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IService<AssignClass, string> service;

        public AssignClassController(IService<AssignClass, string> service, IMapper _mapper)
        {
            this.service = service;
            this._mapper = _mapper;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of AssignClassViewModel (Size >= 0)</returns>
        [HttpGet("get-all-assignclasses")]
        public IEnumerable<AssignClassViewModel> GetAll()
        {
            IEnumerator<AssignClass> assignClasses = service.GetAll().GetEnumerator();

            List<AssignClassViewModel> result = new List<AssignClassViewModel>();

            while (assignClasses.MoveNext())
            {
                result.Add(_mapper.Map<AssignClassViewModel>(assignClasses.Current));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AssignClassViewModel or Null</returns>
        [HttpGet("get-assignclass/{id}")]
        public AssignClassViewModel GetById(string id)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<AssignClassViewModel>(service.GetById(id));
        }

        
        [HttpPost("save-assignclass")]
        public void Post([FromBody] AssignClassViewModel assignClassViewModel)
        {
            service.Save(_mapper.Map<AssignClass>(assignClassViewModel));
            
        }


        [HttpPut("update-assignclass")]
        public void Put([FromBody] AssignClassViewModel assignClassViewModel)
        {
            service.Update(_mapper.Map<AssignClass>(assignClassViewModel));
        }

        [HttpDelete("delete-assignclass/{id}")]
        public void Delete(string id)
        {
            service.Delete(id);
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("validate-assign-class")]
        public JsonResult ValidateAssignClass([FromForm] AssignClassViewModel assignClassViewModel)
        {
            AssignClass assignClass = service.GetById(assignClassViewModel.ClassId);

            if (assignClass != null)
            {

                return new JsonResult(new { isClassExisted = true });
            
            }

            return new JsonResult(new { isSuccess = true });

        }
    }
}
