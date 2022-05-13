using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.ApiHelpers;
using StudentManagementV2.Core.Models;
using StudentManagementV2.Core.PaginatedLists;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementV2.Controllers
{
    [Authorize]
    public class SubjectRegisterController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public SubjectRegisterController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> ListAllSubjectRegisters()
        {
            List<SubjectRegisterWithStudentSubjectSemesterViewModel> result = null;

            string url = null;

            string response = null;

            if (this.User.IsInRole("Student"))
            {
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;

                string currentUserId = _userManager.GetUserId(currentUser);

                //IdentityUser currentIUser = await _userManager.FindByIdAsync(currentUserId);

                //List<string> roles = (List<string>)await _userManager.GetRolesAsync(currentIUser);

                url = $"SubjectRegister/get-all-subject-registers/{currentUserId}";
                response = await ApiHelper.GetAll(url);

                result = JsonConvert.DeserializeObject<List<SubjectRegisterWithStudentSubjectSemesterViewModel>>(response);
            } else
            {
                url = $"SubjectRegister/get-all-subject-registers";
                response = await ApiHelper.GetAll(url);

                result = JsonConvert.DeserializeObject<List<SubjectRegisterWithStudentSubjectSemesterViewModel>>(response);
            }

            return View(result);

        }

        [HttpGet]
        public async Task<ActionResult> CreateNewSubjectRegister()
        {

            SubjectRegisterViewModel subjectRegisterViewModel = new SubjectRegisterViewModel();

            if (this.User.IsInRole("Student"))
            {
                string currentUserId = _userManager.GetUserId(this.User);

                string url = $"Student/get-student-by-identity/{currentUserId}";
                var response = await ApiHelper.GetById(url);

                StudentViewModel studentViewModel = JsonConvert.DeserializeObject<StudentViewModel>(response);

                subjectRegisterViewModel.StudentId = studentViewModel.UserId;

            }

            string urlStudent = "Student/get-all-students";
            string urlSubject = "Subject/get-all-subjects";
            string urlSemester = "Semester/get-all-semesters";

            var responseSubject = await ApiHelper.GetAll(urlSubject);
            var responseSemester = await ApiHelper.GetAll(urlSemester);
            var responseStudent = await ApiHelper.GetAll(urlStudent);

            List<SubjectViewModel> subjectViewModels = JsonConvert.DeserializeObject<List<SubjectViewModel>>(responseSubject);

            List<SemesterViewModel> semesterViewModels = JsonConvert.DeserializeObject<List<SemesterViewModel>>(responseSemester);

            List<StudentViewModel> studentViewModels = JsonConvert.DeserializeObject<List<StudentViewModel>>(responseStudent);

            List<SelectListItem> subjectSelectList = new List<SelectListItem>();
            subjectViewModels.ForEach(s => {
                subjectSelectList.Add(new SelectListItem { Text = s.SubjectName, Value = s.SubjectId });
            });

            List<SelectListItem> semesterSelectList = new List<SelectListItem>();
            semesterViewModels.ForEach(s => {
                semesterSelectList.Add(new SelectListItem { Text = s.SemesterName, Value = s.SemesterId });
            });

            List<SelectListItem> studentSelectList = new List<SelectListItem>();
            studentViewModels.ForEach(s => {
                studentSelectList.Add(new SelectListItem { Text = String.Concat(s.UserName, "[", s.UserId, "]"), Value = s.UserId });
            });

            ViewBag.SubjectRegisterViewModel = subjectRegisterViewModel;
            ViewBag.SubjectSelectList = subjectSelectList;
            ViewBag.SemesterSelectList = semesterSelectList;
            ViewBag.StudentSelectList = studentSelectList;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewSubjectRegister([FromForm] SubjectRegisterViewModel subjectRegisterViewModel)
        {

            string url = "SubjectRegister/save-subject-register";
            var response = await ApiHelper.Create<SubjectRegisterViewModel>(url, subjectRegisterViewModel);

            return RedirectToAction("ListAllSubjectRegisters", "SubjectRegister");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> DeleteSubjectRegister(string studentId, string subjectId, string semesterId, short year)
        {
            
            string url = $"SubjectRegister/delete-subject-register/{studentId}&&{subjectId}&&{semesterId}&&{year}";
            await ApiHelper.Delete(url);

            return RedirectToAction("ListAllSubjectRegisters", "SubjectRegister");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> UpdateSubjectRegister(string studentId, string subjectId, string semesterId, short year)
        {
            string url = $"SubjectRegister/get-subject-register/{studentId}&&{subjectId}&&{semesterId}&&{year}";
            var response = await ApiHelper.GetById(url);

            SubjectRegisterViewModel subjectRegisterViewModel = JsonConvert.DeserializeObject<SubjectRegisterViewModel>(response);

            return View(subjectRegisterViewModel);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> SaveUpdateSubjectRegister(SubjectRegisterViewModel subjectRegisterViewModel)
        {

            string url = "SubjectRegister/update-subject-register";
            var response = await ApiHelper.Update<SubjectRegisterViewModel>(url, subjectRegisterViewModel);

            return RedirectToAction("ListAllSubjectRegisters", "SubjectRegister");
        }

        [HttpGet]
        public async Task<ActionResult> SearchSubjectRegister(string searchBy, string keyword)
        {

            List<SubjectRegisterWithStudentSubjectSemesterViewModel> result = null;

            string url = null;

            string response = null;

            if (this.User.IsInRole("Admin"))
            {
                url = $"SubjectRegister/search-subject-register/{searchBy}&&{keyword}";
                response = await ApiHelper.Search(url);

                result = JsonConvert.DeserializeObject<List<SubjectRegisterWithStudentSubjectSemesterViewModel>>(response);

            } else
            {
                string identityId = _userManager.GetUserId(this.User);

                url = $"SubjectRegister/search-subject-register-of-student/{searchBy}&&{keyword}&&{identityId}";
                response = await ApiHelper.Search(url);

                result = JsonConvert.DeserializeObject<List<SubjectRegisterWithStudentSubjectSemesterViewModel>>(response);

            }

            return View("ListAllSubjectRegisters", result);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> ListAllSubjectRegistersPaging(int page)
        {
            string url = $"SubjectRegister/get-all-subject-registers-paging/{page}";
            var response = await ApiHelper.GetAllPaging<SubjectRegisterWithStudentSubjectSemesterViewModel>(url);

            //PaginatedList<SubjectRegisterWithStudentSubjectSemesterViewModel> result 
            //    = JsonConvert.DeserializeObject<PaginatedList<SubjectRegisterWithStudentSubjectSemesterViewModel>>(response);

            return View(response);

        }
    }
}
