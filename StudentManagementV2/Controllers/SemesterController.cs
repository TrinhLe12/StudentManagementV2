using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.ApiHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementV2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SemesterController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> ListAllSemesters()
        {
            string url = "Semester/get-all-semesters";
            var response = await ApiHelper.GetAll(url);

            List<SemesterViewModel> result = JsonConvert.DeserializeObject<List<SemesterViewModel>>(response);

            return View(result);

        }

        [HttpGet]
        public ActionResult CreateNewSemester()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewSemester([FromForm] SemesterViewModel semesterViewModel)
        {

            string url = "Semester/save-semester";
            var response = await ApiHelper.Create<SemesterViewModel>(url, semesterViewModel);

            return RedirectToAction("ListAllSemesters", "Semester");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSemester(string id)
        {
            
            string url = $"Semester/delete-semester/{id}";
            await ApiHelper.Delete(url);
            

            return RedirectToAction("ListAllSemesters", "Semester");

        }
        [HttpGet]
        public async Task<ActionResult> UpdateSemester(string id)
        {
            string url = $"Semester/get-semester/{id}";
            var response = await ApiHelper.GetById(url);

            SemesterViewModel semesterViewModel = JsonConvert.DeserializeObject<SemesterViewModel>(response);

            return View(semesterViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUpdateSemester(SemesterViewModel semesterViewModel)
        {

            string url = "Semester/update-semester";
            var response = await ApiHelper.Update<SemesterViewModel>(url, semesterViewModel);

            return RedirectToAction("ListAllSemesters", "Semester");
        }
    }
}
