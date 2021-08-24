using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class MyTask
    {
        public int id { get; set; }
        public string name { get; set; }
        public string listOfPerformers { get; set; }
        public DateTime registrationDate { get; set; }
        public Status status { get; set; }
        public DateTime planedTime { get; set; }
        public DateTime realTime { get; set; }
        public List<MyTask> subTasks { get; set; }
        public MyTask masterTask { get; set; }

        public MyTask()
        {
            this.subTasks = new List<MyTask>();
        }

        public string StatusChangeCheck(Status oldStatus)
        {
            string res = "";
            List<Status> availableStatus = this.AvailableStatus(oldStatus);
            if (!availableStatus.Contains(status))
                res = String.Format("Недопустимо изменять статус с {0} на {1}", oldStatus, status);
            return res;
        }

        public List<Status> AvailableStatus(Status oldStatus)
        {
            List<Status> res = new List<Status>();
            switch (oldStatus)
            {
                case Status.Назначена:
                    res.Add(Status.Выполняется);
                    break;
                case Status.Выполняется:
                    res.Add(Status.Приостановлена);
                    res.Add(Status.Завершена);
                    break;
                case Status.Приостановлена:
                    res.Add(Status.Выполняется);
                    break;
            }
            return res;
        }

        public enum Status
        {
            Назначена = 1,
            Выполняется = 2,
            Приостановлена = 3,
            Завершена = 4
        }
    }
}
