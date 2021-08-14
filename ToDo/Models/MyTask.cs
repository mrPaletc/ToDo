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
        public string status { get; set; }
        public DateTime planedTime { get; set; }
        public DateTime realTime { get; set; }
        public List<MyTask> subTasks { get; set; }
        public MyTask masterTask { get; set; }

        public MyTask()
        {
            this.subTasks = new List<MyTask>();
        }

        public List<string> AvailableStatus()
        {
            List<string> availabe = new List<string>();
            switch (status)
            {
                case "Назначена":
                    availabe.Add("Выполняется");
                    break;
                case "Выполняется":
                    if (CanBeFinished())
                    {
                        availabe.Add("Завершена");
                    }
                    availabe.Add("Приостановлена");
                    break;
                case "Приостановлена":
                    availabe.Add("Выполняется");
                    break;
            }
            return availabe;
        }

        private bool CanBeFinished()
        {
            if (status == "Выполняется")
            {
                if(subTasks.Count > 0)
                {
                    foreach (MyTask task in subTasks)
                    {
                        if(task.CanBeFinished() == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public TimeSpan SubTasksRealTime()
        {
            TimeSpan timeSpan = new TimeSpan();
            return timeSpan;
        }
    }
}
