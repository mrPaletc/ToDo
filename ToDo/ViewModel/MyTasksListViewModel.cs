using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.ViewModel
{
    public class MyTasksListViewModel
    {
        public IEnumerable<MyTask> allTasks { get; set; }

        public MyTask curTask { get; set; }

    }
}
