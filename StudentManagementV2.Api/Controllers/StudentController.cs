using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.Business.Services;
using StudentManagementV2.Business.Validators;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentManagementV2.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IService<Student, string> service;

        private readonly IStudentService _studentService;

        private readonly ICommonValidator commonValidator;

        public StudentController(IService<Student, string> service, IMapper _mapper, IStudentService studentService, ICommonValidator commonValidator)
        {
            this.service = service;
            this._mapper = _mapper;
            this._studentService = studentService;
            this.commonValidator = commonValidator;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of StudentViewModel (Size >= 0)</returns>
        [HttpGet("get-all-students")]
        public IEnumerable<StudentViewModel> GetAll()
        {
            IEnumerator<Student> students = service.GetAll().GetEnumerator();

            List<StudentViewModel> result = new List<StudentViewModel>();

            while (students.MoveNext())
            {
                result.Add(_mapper.Map<StudentViewModel>(students.Current));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StudentViewModel or Null</returns>
        [HttpGet("get-student/{id}")]
        public StudentViewModel GetById(string id)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<StudentViewModel>(service.GetById(id));
        }

        [HttpGet("get-student-by-identity/{identityId}")]
        public StudentViewModel GetByIdentityId(string identityId)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<StudentViewModel>(_studentService.GetStudentByIdentityId(identityId));
        }


        [HttpPost("save-student")]
        public void Post([FromBody] StudentViewModel studentViewModel)
        {
            service.Save(_mapper.Map<Student>(studentViewModel));

        }


        [HttpPut("update-student")]
        public void Put([FromBody] StudentViewModel studentViewModel)
        {
            service.Update(_mapper.Map<Student>(studentViewModel));
        }

        [HttpDelete("delete-student/{id}")]
        public void Delete(string id)
        {
            service.Delete(id);
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("validate-new-student")]
        public JsonResult ValidateStudent([FromForm]StudentViewModel studentViewModel)
        {
            Student student = service.GetById(studentViewModel.UserId);

            bool isBirthdatePast = commonValidator.PastDateValidator((DateTime)studentViewModel.Birthdate);

            if (student != null)
            {
                
                if (isBirthdatePast)
                {
                    return new JsonResult(new { isStudentExisted = true, isBirthdatePast = true });
                }

                return new JsonResult(new { isStudentExisted = true, isBirthdatePast = false });
            }

            if (isBirthdatePast)
            {
                return new JsonResult(new { isSuccess = true });
            }

            return new JsonResult(new { isStudentExisted = false, isBirthdatePast = false });

        }

        [HttpGet("get-student-by-assign-class/{classId}")]
        public List<StudentViewModel> GetByAssignClass(string classId)
        {
            return _mapper.Map<List<StudentViewModel>>(_studentService.GetStudentByAssignClass(classId));
        }
    }
}
