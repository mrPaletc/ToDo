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
            _allMyTasks.Create(task);
            return View();
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
            int masterTaskId = (int)TempData["masterID"];
            MyTask masterTask = _allMyTasks.getMyTask(masterTaskId);

            _allMyTasks.Create(task);
            masterTask.subTasks.Add(task);
            _allMyTasks.Update(masterTask);
            return RedirectToAction("List");
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
            if (id != null)
            {
                MyTasksListViewModel obj = new MyTasksListViewModel();
                obj.allTasks = _allMyTasks.AllMyTasks;
                obj.curTask = _allMyTasks.getMyTask((int)id);
                ViewBag.MyTask = _allMyTasks;
                return View("List", obj);

            }
            return NotFound();
        }
    }
}
