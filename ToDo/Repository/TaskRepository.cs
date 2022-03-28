using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TaskRepository : IJob
    {
        private readonly AppDbContent appDbContent;

        public TaskRepository(AppDbContent appDbContent)
        {
            this.appDbContent = appDbContent;
        }
        public IEnumerable<Job> AllJobs
        {
            get
            {
                IEnumerable<Job> res = appDbContent.Task.Include(c => c.SubJobs);
                return res;
            }
        }
        public  Job getJob(int JobId) => appDbContent.Task.Include(c => c.SubJobs).FirstOrDefault(p => p.id == JobId);
        public  void Create(Job task)
        {
            appDbContent.Task.Add(task);
            appDbContent.SaveChanges();
        }
        public async void Delete(int id)
        {
        Job task = getJob(id);

            appDbContent.Task.Remove(task);
            await appDbContent.SaveChangesAsync();
        }
        public async void Update(Job task)
        {
            appDbContent.Task.Update(task);
            await appDbContent.SaveChangesAsync();
        }




    }
}
