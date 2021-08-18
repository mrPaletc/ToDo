using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TaskRepository : IMyTask
    {
        private readonly AppDbContent appDbContent;

        public TaskRepository(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }
        public IEnumerable<MyTask> AllMyTasks
        {
            get
            {
                IEnumerable<MyTask> res = appDbContent.Task.Include(c => c.subTasks);
                return res;
            }
        }
        public MyTask getMyTask(int myTaskId) => appDbContent.Task.FirstOrDefault(p => p.id == myTaskId);

        public void Create(MyTask task)
        {
            appDbContent.Task.Add(task);
            appDbContent.SaveChanges();
        }
        public void Delete(int id)
        {
        MyTask task = getMyTask(id);
                
            appDbContent.Task.Remove(task);
            appDbContent.SaveChanges();
        }

        public void Update(MyTask task)
        {
            appDbContent.Task.Update(task);
            appDbContent.SaveChanges();
        }




    }
}
