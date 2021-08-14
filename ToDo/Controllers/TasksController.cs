﻿using Microsoft.AspNetCore.Mvc;
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
            obj.curTask = _allMyTasks.AllMyTasks.First();
            ViewBag.MyTask = _allMyTasks;
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
                MyTasksListViewModel obj = new MyTasksListViewModel();
                obj.curTask = _allMyTasks.getMyTask((int)masterTaskId);

                if (obj.curTask != null)
                    return View(obj);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubTask(int masterTaskId, MyTask task)
        {
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

                if (obj.curTask != null)
                    return View(obj);
            }
            return NotFound();
        }
       
        [HttpPost]
        public async Task<IActionResult> Update(MyTask task)
        {
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