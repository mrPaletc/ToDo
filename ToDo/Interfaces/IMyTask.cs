using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Interfaces
{
    public interface IMyTask
    {
        IEnumerable<MyTask> AllMyTasks { get;}
        MyTask getMyTask(int myTaskId);
        void Create(MyTask task);
        void Update(MyTask task);
        void Delete(int id);
    }
}
