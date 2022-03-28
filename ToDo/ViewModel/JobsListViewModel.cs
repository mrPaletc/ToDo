using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.ViewModel
{
    public class JobsListViewModel
    {
        public IEnumerable<Job> allTasks { get; set; }

        public Job curTask { get; set; }

    }
}
