using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Interfaces;
using ToDo.Models;
using ToDo.ViewModel;

namespace ToDo.Controllers
{
    public class TasksController : Controller
    {
        private IJob _allJobs;

        public TasksController(IJob iJob)
        {
            _allJobs = iJob;
        }

        public ViewResult List()
        {
            JobsListViewModel obj = new JobsListViewModel();
            obj.allTasks = _allJobs.AllJobs;
            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(Job task)
        {
            try
            {
                _allJobs.Create(task);
                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> CreateSubJob(int? masterTaskId)
        {
            if (masterTaskId != null)
            {
                if (_allJobs.getJob((int)masterTaskId) != null)
                {
                    TempData["masterID"] = (int)masterTaskId;
                    return View();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubJob(Job task)
        {
            try
            {

                int masterTaskId = (int)TempData["masterID"];
                Job masterTask = _allJobs.getJob(masterTaskId);

                _allJobs.Create(task);
                masterTask.SubJobs.Add(task);
                _allJobs.Update(masterTask);
                return RedirectToAction("List");
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Job task = _allJobs.getJob((int)id);
                if (task.SubJobs.Count > 0)
                {
                    ViewBag.Message = "Нельзя удалить задачу у которой есть подзадачи"; 
                    JobsListViewModel obj = new JobsListViewModel();
                    obj.allTasks = _allJobs.AllJobs;
                    return View("List",obj);
                }
                if (task != null)
                {
                    _allJobs.Delete((int)id);
                    return RedirectToAction("List");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Update(int? id)
        {
            try
            {
                if (id != null)
                {
                    JobsListViewModel obj = new JobsListViewModel();
                    obj.curTask = _allJobs.getJob((int)id);
                    TempData["oldStatus"] = obj.curTask.status;
                    if (obj.curTask != null)
                    {
                        return View(obj);
                    }
                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> Update(Job task)
        {
            Status oldStatus = (Status)TempData["oldStatus"];
            ViewBag.UpdateCheck = task.StatusChangeCheck(oldStatus);
            if (!String.IsNullOrEmpty(ViewBag.UpdateCheck))
            {
                JobsListViewModel obj = new JobsListViewModel();
                obj.curTask = _allJobs.getJob(task.id);
                TempData["oldStatus"] = oldStatus;
                return View(obj);
            }
            _allJobs.Update(task);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {

                if (id != null)
                {
                    JobsListViewModel obj = new JobsListViewModel();
                    obj.allTasks = _allJobs.AllJobs;
                    obj.curTask = _allJobs.getJob((int)id);
                    ViewBag.Job = _allJobs;
                    ViewBag.SubJobsSumPlanedTime = SubJobsSumPlanedTime(obj.curTask);
                    ViewBag.SubJobsSumRealTime = SubJobsSumRealTime(obj.curTask);
                    return View("List", obj);

                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        private TimeSpan SubJobsSumPlanedTime(Job masterTask)
        {
            TimeSpan res = new TimeSpan();
            foreach(Job task in masterTask.SubJobs)
            {
                res =  res + task.planedTime;
                if (task.SubJobs.Count > 0)
                {
                    res = res + SubJobsSumPlanedTime(task);
                }
            }
            return res;
        }
        private TimeSpan SubJobsSumRealTime(Job masterTask)
        {
            TimeSpan res = new TimeSpan();
            foreach (Job task in masterTask.SubJobs)
            {
                res = res + task.realTime;
                if (task.SubJobs.Count > 0)
                {
                    res = res + SubJobsSumRealTime(task);
                }
            }
            return res;
        }
        
        
    }
}
