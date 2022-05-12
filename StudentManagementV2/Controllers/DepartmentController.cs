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
    public class DepartmentController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> ListAllDepartments()
        {
            string url = "Department/get-all-departments";
            var response = await ApiHelper.GetAll(url);

            List<DepartmentViewModel> result = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(response);

            return View(result);

        }

        [HttpGet]
        public ActionResult CreateNewDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewDepartment([FromForm] DepartmentViewModel departmentViewModel)
        {

            string url = "Department/save-department";
            var response = await ApiHelper.Create<DepartmentViewModel>(url, departmentViewModel);

            return RedirectToAction("ListAllDepartments", "Department");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            
            string url = $"Department/delete-department/{id}";
            // Department/delete/5
            await ApiHelper.Delete(url);
            

            return RedirectToAction("ListAllDepartments", "Department");

        }
        [HttpGet]
        public async Task<ActionResult> UpdateDepartment(string id)
        {
            string url = $"Department/get-department/{id}";
            var response = await ApiHelper.GetById(url);

            DepartmentViewModel departmentViewModel = JsonConvert.DeserializeObject<DepartmentViewModel>(response);

            return View(departmentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUpdateDepartment(DepartmentViewModel departmentViewModel)
        {

            string url = "Department/update-department";
            var response = await ApiHelper.Update<DepartmentViewModel>(url, departmentViewModel);

            return RedirectToAction("ListAllDepartments", "Department");
        }
    }
}
