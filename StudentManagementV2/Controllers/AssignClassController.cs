using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentManagementV2.Api.ViewModels;
using StudentManagementV2.ApiHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementV2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AssignClassController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> ListAllAssignClasses()
        {
            string url = "AssignClass/get-all-assignclasses";
            var response = await ApiHelper.GetAll(url);

            List<AssignClassViewModel> result = JsonConvert.DeserializeObject<List<AssignClassViewModel>>(response);

            return View(result);

        }

        [HttpGet]
        public async Task<ActionResult> CreateNewAssignClass()
        {

            string url = "Department/get-all-departments";
            var response = await ApiHelper.GetAll(url);

            List<DepartmentViewModel> departmentViewModels = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(response);

            List<SelectListItem> departmentSelectList = new List<SelectListItem>();

            departmentViewModels.ForEach(d => {
                departmentSelectList.Add(new SelectListItem { Text = d.DeptName, Value = d.DeptId });
            });

            ViewBag.DepartmentSelectList = departmentSelectList;

            return View();

        }

        [HttpPost]
        public async Task<ActionResult> SaveNewAssignClass([FromForm] AssignClassViewModel assignClassViewModel)
        {

            string url = "AssignClass/save-assignclass";
            var response = await ApiHelper.Create<AssignClassViewModel>(url, assignClassViewModel);

            return RedirectToAction("ListAllAssignClasses", "AssignClass");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAssignClass(string id)
        {
            
            string url = $"AssignClass/delete-assignclass/{id}";
            await ApiHelper.Delete(url);

            return RedirectToAction("ListAllAssignClasses", "AssignClass");

        }
        [HttpGet]
        public async Task<ActionResult> UpdateAssignClass(string id)
        {
            string url = $"AssignClass/get-assignclass/{id}";
            var response = await ApiHelper.GetById(url);

            AssignClassViewModel assignClassViewModel = JsonConvert.DeserializeObject<AssignClassViewModel>(response);

            string urlDepartment = "Department/get-all-departments";
            var responseDepartment = await ApiHelper.GetAll(urlDepartment);

            List<DepartmentViewModel> departmentViewModels = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(responseDepartment);

            List<SelectListItem> departmentSelectList = new List<SelectListItem>();

            departmentViewModels.ForEach(d => {

                if (assignClassViewModel.DeptId != null && assignClassViewModel.DeptId.Equals(d.DeptId))
                {
                    departmentSelectList.Add(new SelectListItem { Text = d.DeptName, Value = d.DeptId, Selected = true });
                } else
                {
                    departmentSelectList.Add(new SelectListItem { Text = d.DeptName, Value = d.DeptId });
                }
                
            });

            //ViewBag.AssignClassViewModel = assignClassViewModel;
            ViewBag.DepartmentSelectList = departmentSelectList;

            return View(assignClassViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUpdateAssignClass(AssignClassViewModel assignClassViewModel)
        {

            string url = "AssignClass/update-assignclass";
            var response = await ApiHelper.Update<AssignClassViewModel>(url, assignClassViewModel);

            return RedirectToAction("ListAllAssignClasses", "AssignClass");
        }
    }
}
