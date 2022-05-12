using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.ApiHelpers;
using StudentManagementV2.Core.Constants;
using StudentManagementV2.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementV2.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> ListAllStudents()
        {

            string url = "Student/get-all-students";
            var response = await ApiHelper.GetAll(url);

            List<StudentViewModel> result = JsonConvert.DeserializeObject<List<StudentViewModel>>(response);

            return View(result);

        }

        [HttpGet]
        public async Task<ActionResult> CreateNewStudent(StudentViewModel studentViewModel)
        {
            string url = "AssignClass/get-all-assignclasses";
            var response = await ApiHelper.GetAll(url);

            List<AssignClassViewModel> assignClassViewModels = JsonConvert.DeserializeObject<List<AssignClassViewModel>>(response);

            List<SelectListItem> assignClassSelectList = new List<SelectListItem>();

            assignClassViewModels.ForEach(ac => {
                assignClassSelectList.Add(new SelectListItem { Text=ac.ClassName, Value=ac.ClassId });
            });

            Role role = new Role();
            role.RoleId = Constant.RoleId.R01.ToString();

            ViewBag.StudentViewModel = studentViewModel;
            ViewBag.AssignClassSelectList = assignClassSelectList;
            ViewBag.Role = role;
            //ViewBag.AssignClassViewModels = assignClassViewModels;
            
            return View(new StudentViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewStudent([FromForm] StudentViewModel studentViewModel)
        {

            string url = "Student/save-student";
            var response = await ApiHelper.Create<StudentViewModel>(url, studentViewModel);

            if (this.User.IsInRole("Admin"))
            {
                return RedirectToAction("ListAllStudents", "Student");
            }

            if (!string.IsNullOrEmpty(studentViewModel.ReturnUrl))
                return Redirect(studentViewModel.ReturnUrl);
            else
                return RedirectToAction("Index", "Home");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> DeleteStudent(string id)
        {

            string urlGet = $"Student/get-student/{id}";
            var response = await ApiHelper.GetById(urlGet);

            StudentViewModel studentViewModel = JsonConvert.DeserializeObject<StudentViewModel>(response);

            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(studentViewModel.IdentityId));

            string urlDelete = $"Student/delete-student/{id}";
            await ApiHelper.Delete(urlDelete);

            return RedirectToAction("ListAllStudents", "Student");

        }

        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<ActionResult> UpdatePersonalInfo()
        {
            string identityId = _userManager.GetUserId(this.User);

            string url = $"Student/get-student-by-identity/{identityId}";
            var response = await ApiHelper.GetById(url);

            StudentViewModel studentViewModel = JsonConvert.DeserializeObject<StudentViewModel>(response);

            string urlClass = "AssignClass/get-all-assignclasses";
            var responseClass = await ApiHelper.GetAll(urlClass);

            List<AssignClassViewModel> assignClassViewModels = JsonConvert.DeserializeObject<List<AssignClassViewModel>>(responseClass);

            List<SelectListItem> assignClassSelectList = new List<SelectListItem>();

            assignClassViewModels.ForEach(ac => {

                if (studentViewModel.ClassId != null && studentViewModel.ClassId.Equals(ac.ClassId)) {
                    assignClassSelectList.Add(new SelectListItem { Text = ac.ClassName, Value = ac.ClassId, Selected = true });
                } else
                {
                    assignClassSelectList.Add(new SelectListItem { Text = ac.ClassName, Value = ac.ClassId });
                }

            });

            ViewBag.AssignClassSelectList = assignClassSelectList;

            return View("UpdateStudent", studentViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> UpdateStudent(string id)
        {
            string url = $"Student/get-student/{id}";
            var response = await ApiHelper.GetById(url);

            StudentViewModel studentViewModel = JsonConvert.DeserializeObject<StudentViewModel>(response);

            string urlClass = "AssignClass/get-all-assignclasses";
            var responseClass = await ApiHelper.GetAll(urlClass);

            List<AssignClassViewModel> assignClassViewModels = JsonConvert.DeserializeObject<List<AssignClassViewModel>>(responseClass);

            List<SelectListItem> assignClassSelectList = new List<SelectListItem>();

            assignClassViewModels.ForEach(ac => {

                if (studentViewModel.ClassId != null && studentViewModel.ClassId.Equals(ac.ClassId))
                {
                    assignClassSelectList.Add(new SelectListItem { Text = ac.ClassName, Value = ac.ClassId, Selected = true });
                }
                else
                {
                    assignClassSelectList.Add(new SelectListItem { Text = ac.ClassName, Value = ac.ClassId });
                }

            });

            ViewBag.AssignClassSelectList = assignClassSelectList;

            return View(studentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUpdateStudent(StudentViewModel studentViewModel)
        {

            string url = "Student/update-student";
            var response = await ApiHelper.Update<StudentViewModel>(url, studentViewModel);

            if (this.User.IsInRole("Student"))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ListAllStudents", "Student");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetStudentByAssignClass(string classId)
        {

            string url = $"Student/get-student-by-assign-class/{classId}";
            var response = await ApiHelper.GetById(url);

            List<StudentViewModel> studentViewModels = JsonConvert.DeserializeObject<List<StudentViewModel>>(response);

            return View("ListAllStudents", studentViewModels);
        }

    }
}
