using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface IJob
    {
        IEnumerable<Job> AllJobs { get;}
        Job getJob(int JobId);
        void Create(Job task);
        void Update(Job task);
        void Delete(int id);
    }
}
