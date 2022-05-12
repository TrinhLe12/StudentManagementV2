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
    public class SemesterController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IService<Semester, string> service;

        public SemesterController(IService<Semester, string> service, IMapper _mapper)
        {
            this.service = service;
            this._mapper = _mapper;
        }

        [HttpGet("get-all-semesters")]
        public IEnumerable<SemesterViewModel> GetAll()
        {
            IEnumerator<Semester> semesters = service.GetAll().GetEnumerator();

            List<SemesterViewModel> result = new List<SemesterViewModel>();

            while (semesters.MoveNext())
            {
                result.Add(_mapper.Map<SemesterViewModel>(semesters.Current));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SemesterViewModel or Null</returns>
        [HttpGet("get-semester/{id}")]
        public SemesterViewModel GetById(string id)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<SemesterViewModel>(service.GetById(id));
        }

        [HttpPost("save-semester")]
        public void Post([FromBody] SemesterViewModel semesterViewModel)
        {
            service.Save(_mapper.Map<Semester>(semesterViewModel));

        }

        [HttpPut("update-semester")]
        public void Put([FromBody] SemesterViewModel semesterViewModel)
        {
            service.Update(_mapper.Map<Semester>(semesterViewModel));
        }

        [HttpDelete("delete-semester/{id}")]
        public void Delete(string id)
        {
            service.Delete(id);
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("validate-semester")]
        public JsonResult ValidateSemester([FromForm] SemesterViewModel semesterViewModel)
        {
            Semester semester = service.GetById(semesterViewModel.SemesterId);

            if (semester != null)
            {

                return new JsonResult(new { isSemesterExisted = true });

            }

            return new JsonResult(new { isSuccess = true });

        }
    }
}
