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
    public class SubjectController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> ListAllSubjects()
        {
            string url = "Subject/get-all-subjects";
            var response = await ApiHelper.GetAll(url);

            List<SubjectViewModel> result = JsonConvert.DeserializeObject<List<SubjectViewModel>>(response);

            return View(result);

        }

        [HttpGet]
        public ActionResult CreateNewSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewSubject([FromForm] SubjectViewModel subjectViewModel)
        {

            string url = "Subject/save-subject";
            var response = await ApiHelper.Create<SubjectViewModel>(url, subjectViewModel);

            return RedirectToAction("ListAllSubjects", "Subject");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSubject(string id)
        {
            
            string url = $"Subject/delete-subject/{id}";
            await ApiHelper.Delete(url);
            

            return RedirectToAction("ListAllSubjects", "Subject");

        }
        [HttpGet]
        public async Task<ActionResult> UpdateSubject(string id)
        {
            string url = $"Subject/get-subject/{id}";
            var response = await ApiHelper.GetById(url);

            SubjectViewModel subjectViewModel = JsonConvert.DeserializeObject<SubjectViewModel>(response);

            return View(subjectViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUpdateSubject(SubjectViewModel subjectViewModel)
        {

            string url = "Subject/update-subject";
            var response = await ApiHelper.Update<SubjectViewModel>(url, subjectViewModel);

            return RedirectToAction("ListAllSubjects", "Subject");
        }
    }
}
