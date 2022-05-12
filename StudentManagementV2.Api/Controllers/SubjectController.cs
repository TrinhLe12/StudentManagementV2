using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.Business.Services;
using StudentManagementV2.Core.Models;
using System.Collections.Generic;

namespace StudentManagementV2.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {

        private readonly IMapper _mapper;

        private readonly IService<Subject, string> service;

        public SubjectController(IService<Subject, string> service, IMapper _mapper)
        {
            this.service = service;
            this._mapper = _mapper;
        }

        [HttpGet("get-all-subjects")]
        public IEnumerable<SubjectViewModel> GetAll()
        {
            IEnumerator<Subject> subjects = service.GetAll().GetEnumerator();

            List<SubjectViewModel> result = new List<SubjectViewModel>();

            while (subjects.MoveNext())
            {
                result.Add(_mapper.Map<SubjectViewModel>(subjects.Current));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SubjectViewModel or Null</returns>
        [HttpGet("get-subject/{id}")]
        public SubjectViewModel GetById(string id)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<SubjectViewModel>(service.GetById(id));
        }

        [HttpPost("save-subject")]
        public void Post([FromBody] SubjectViewModel subjectViewModel)
        {
            service.Save(_mapper.Map<Subject>(subjectViewModel));

        }

        [HttpPut("update-subject")]
        public void Put([FromBody] SubjectViewModel subjectViewModel)
        {
            service.Update(_mapper.Map<Subject>(subjectViewModel));
        }

        [HttpDelete("delete-subject/{id}")]
        public void Delete(string id)
        {
            service.Delete(id);
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("validate-subject")]
        public JsonResult ValidateSubject([FromForm] SubjectViewModel subjectViewModel)
        {
            Subject subject = service.GetById(subjectViewModel.SubjectId);

            if (subject != null)
            {

                return new JsonResult(new { isSubjectExisted = true });

            }

            return new JsonResult(new { isSuccess = true });

        }
    }
}
