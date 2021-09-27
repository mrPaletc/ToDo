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
        private IMyTask _allMyTasks;

        public TasksController(IMyTask iMyTask)
        {
            _allMyTasks = iMyTask;
        }

        public ViewResult List()
        {
            MyTasksListViewModel obj = new MyTasksListViewModel();
            obj.allTasks = _allMyTasks.AllMyTasks;
            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(MyTask task)
        {
            try
            {
                _allMyTasks.Create(task);
                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> CreateSubTask(int? masterTaskId)
        {
            if (masterTaskId != null)
            {
                if (_allMyTasks.getMyTask((int)masterTaskId) != null)
                {
                    TempData["masterID"] = (int)masterTaskId;
                    return View();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubTask(MyTask task)
        {
            try
            {

                int masterTaskId = (int)TempData["masterID"];
                MyTask masterTask = _allMyTasks.getMyTask(masterTaskId);

                _allMyTasks.Create(task);
                masterTask.subTasks.Add(task);
                _allMyTasks.Update(masterTask);
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
                MyTask task = _allMyTasks.getMyTask((int)id);
                if (task.subTasks.Count > 0)
                {
                    ViewBag.Message = "Нельзя удалить задачу у которой есть подзадачи"; 
                    MyTasksListViewModel obj = new MyTasksListViewModel();
                    obj.allTasks = _allMyTasks.AllMyTasks;
                    return View("List",obj);
                }
                if (task != null)
                {
                    _allMyTasks.Delete((int)id);
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
                    MyTasksListViewModel obj = new MyTasksListViewModel();
                    obj.curTask = _allMyTasks.getMyTask((int)id);
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
        public async Task<IActionResult> Update(MyTask task)
        {
            MyTask.Status oldStatus = (MyTask.Status)TempData["oldStatus"];
            ViewBag.UpdateCheck = task.StatusChangeCheck(oldStatus);
            if (!String.IsNullOrEmpty(ViewBag.UpdateCheck))
            {
                MyTasksListViewModel obj = new MyTasksListViewModel();
                obj.curTask = _allMyTasks.getMyTask(task.id);
                TempData["oldStatus"] = oldStatus;
                return View(obj);
            }
            _allMyTasks.Update(task);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {

                if (id != null)
                {
                    MyTasksListViewModel obj = new MyTasksListViewModel();
                    obj.allTasks = _allMyTasks.AllMyTasks;
                    obj.curTask = _allMyTasks.getMyTask((int)id);
                    ViewBag.MyTask = _allMyTasks;
                    ViewBag.SubTasksSumPlanedTime = SubTasksSumPlanedTime(obj.curTask);
                    ViewBag.SubTasksSumRealTime = SubTasksSumRealTime(obj.curTask);
                    return View("List", obj);

                }
                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        private TimeSpan SubTasksSumPlanedTime(MyTask masterTask)
        {
            TimeSpan res = new TimeSpan();
            foreach(MyTask task in masterTask.subTasks)
            {
                res =  res + task.planedTime;
                if (task.subTasks.Count > 0)
                {
                    res = res + SubTasksSumPlanedTime(task);
                }
            }
            return res;
        }
        private TimeSpan SubTasksSumRealTime(MyTask masterTask)
        {
            TimeSpan res = new TimeSpan();
            foreach (MyTask task in masterTask.subTasks)
            {
                res = res + task.realTime;
                if (task.subTasks.Count > 0)
                {
                    res = res + SubTasksSumRealTime(task);
                }
            }
            return res;
        }
        
        
    }
}
