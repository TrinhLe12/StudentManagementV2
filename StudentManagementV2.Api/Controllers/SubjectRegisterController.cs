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
    public class SubjectRegisterController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IService<SubjectRegister, object> service;
        private readonly ISubjectRegisterService subjectRegisterService;

        public SubjectRegisterController(IService<SubjectRegister, object> service, IMapper _mapper, ISubjectRegisterService subjectRegisterService)
        {
            this.service = service;
            this._mapper = _mapper;
            this.subjectRegisterService = subjectRegisterService;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of SubjectRegisterViewModel (Size >= 0)</returns>
        [HttpGet("get-all-subject-registers/{currentUserId}")]
        public IEnumerable<SubjectRegisterWithStudentSubjectSemesterViewModel> GetAll(string currentUserId)
        {
            IEnumerator<SubjectRegister> subjectRegisters = subjectRegisterService.GetAllWithStudentSubjectSemester(currentUserId).GetEnumerator();

            List<SubjectRegisterWithStudentSubjectSemesterViewModel> result = new List<SubjectRegisterWithStudentSubjectSemesterViewModel>();

            while (subjectRegisters.MoveNext())
            {
                result.Add(_mapper.Map<SubjectRegisterWithStudentSubjectSemesterViewModel>(subjectRegisters.Current));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of SubjectRegisterViewModel (Size >= 0)</returns>
        [HttpGet("get-all-subject-registers")]
        public IEnumerable<SubjectRegisterWithStudentSubjectSemesterViewModel> GetAll()
        {
            IEnumerator<SubjectRegister> subjectRegisters = subjectRegisterService.GetAllWithStudentSubjectSemester().GetEnumerator();

            List<SubjectRegisterWithStudentSubjectSemesterViewModel> result = new List<SubjectRegisterWithStudentSubjectSemesterViewModel>();

            while (subjectRegisters.MoveNext())
            {
                result.Add(_mapper.Map<SubjectRegisterWithStudentSubjectSemesterViewModel>(subjectRegisters.Current));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SubjectRegisterViewModel or Null</returns>
        [HttpGet("get-subject-register/{studentId}&&{subjectId}&&{semesterId}&&{year}")]
        public SubjectRegisterViewModel GetById(string studentId, string subjectId, string semesterId, short year)
        {
            // If source == null -> Automapper returns null
            return _mapper.Map<SubjectRegisterViewModel>(subjectRegisterService.GetByCompositeId(studentId, subjectId, semesterId, year));
        }


        [HttpPost("save-subject-register")]
        public void Post([FromBody] SubjectRegisterViewModel subjectRegisterViewModel)
        {
            service.Save(_mapper.Map<SubjectRegister>(subjectRegisterViewModel));

        }


        [HttpPut("update-subject-register")]
        public void Put([FromBody] SubjectRegisterViewModel subjectRegisterViewModel)
        {
            service.Update(_mapper.Map<SubjectRegister>(subjectRegisterViewModel));
        }

        [HttpDelete("delete-subject-register/{studentId}&&{subjectId}&&{semesterId}&&{year}")]
        public void Delete(string studentId, string subjectId, string semesterId, short year)
        {
            subjectRegisterService.DeleteByCompositeId(studentId, subjectId, semesterId, year);
        }

        [HttpGet("search-subject-register/{searchBy}&&{keyword}")]
        public IEnumerable<SubjectRegisterWithStudentSubjectSemesterViewModel> SearchSubjectRegister (string searchBy, string keyword)
        {

            List<SubjectRegisterWithStudentSubjectSemesterViewModel> result = new List<SubjectRegisterWithStudentSubjectSemesterViewModel>();

            IEnumerator<SubjectRegister> subjectRegisters = null;

            switch (searchBy)
            {
                case "Subject":
                    
                    subjectRegisters = subjectRegisterService.SearchBySubject(keyword).GetEnumerator();

                    break;

                case "Semester":

                    subjectRegisters = subjectRegisterService.SearchBySemester(keyword).GetEnumerator();

                    break;

                case "Year":

                    subjectRegisters = subjectRegisterService.SearchByYear(keyword).GetEnumerator();

                    break;

                default:

                    Console.WriteLine("Search criteria not exist!");
                    
                    break;
            }

            if (subjectRegisters != null)
            {
                while (subjectRegisters.MoveNext())
                {
                    result.Add(_mapper.Map<SubjectRegisterWithStudentSubjectSemesterViewModel>(subjectRegisters.Current));
                }
            }

            return result;

        }

        [HttpGet("search-subject-register-of-student/{searchBy}&&{keyword}&&{identityId}")]
        public IEnumerable<SubjectRegisterWithStudentSubjectSemesterViewModel> SearchSubjectRegisterOfStudent (string searchBy, string keyword, string identityId)
        {

            List<SubjectRegisterWithStudentSubjectSemesterViewModel> result = new List<SubjectRegisterWithStudentSubjectSemesterViewModel>();

            IEnumerator<SubjectRegister> subjectRegisters = null;

            switch (searchBy)
            {
                case "Subject":

                    subjectRegisters = subjectRegisterService.SearchBySubject(keyword, identityId).GetEnumerator();

                    break;

                case "Semester":

                    subjectRegisters = subjectRegisterService.SearchBySemester(keyword, identityId).GetEnumerator();

                    break;

                case "Year":

                    subjectRegisters = subjectRegisterService.SearchByYear(keyword, identityId).GetEnumerator();

                    break;

                default:

                    Console.WriteLine("Search criteria not exist!");

                    break;
            }

            if (subjectRegisters != null)
            {
                while (subjectRegisters.MoveNext())
                {
                    result.Add(_mapper.Map<SubjectRegisterWithStudentSubjectSemesterViewModel>(subjectRegisters.Current));
                }
            }

            return result;

        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("validate-subject-register")]
        public JsonResult ValidateSubjectRegister([FromForm] SubjectRegisterViewModel subjectRegisterViewModel)
        {
            SubjectRegister subjectRegister = subjectRegisterService.GetByCompositeId(subjectRegisterViewModel.StudentId, 
                subjectRegisterViewModel.SubjectId, 
                subjectRegisterViewModel.SemesterId, 
                subjectRegisterViewModel.Year);

            if (subjectRegister != null)
            {

                return new JsonResult(new { isSubjectRegisterExisted = true });
            }

            return new JsonResult(new { isSuccess = true });
        }
    }
}
