using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Job
    {
        public int id { get; set; }
        public string name { get; set; }
        public IEnumerable<Person> listOfPerformers { get; set; }
        public DateTime registrationDate { get; set; }
        public Status status { get; set; }
        public TimeSpan planedTime { get; set; }
        public TimeSpan realTime { get; set; }
        public List<Job> SubJobs { get; set; }
        public Job masterTask { get; set; }

        public Job()
        {
            this.SubJobs = new List<Job>();
        }

        public string StatusChangeCheck(Status oldStatus)
        {
            string res = "";
            List<Status> availableStatus = this.AvailableStatus(oldStatus);
            if (!availableStatus.Contains(status))
                res = String.Format("Недопустимо изменять статус с {0} на {1}", oldStatus, status);
            return res;
        }

        private List<Status> AvailableStatus(Status oldStatus)
        {
            List<Status> res = new List<Status>();
            switch (oldStatus)
            {
                case Status.New:
                    res.Add(Status.Performed);
                    break;
                case Status.Performed:
                    res.Add(Status.Suspended);
                    res.Add(Status.Completed);
                    break;
                case Status.Suspended:
                    res.Add(Status.Performed);
                    break;
            }
            return res;
        }

    }

    public enum Status
    {
        New = 1,
        Performed = 2,
        Suspended = 3,
        Completed = 4
    }
}
