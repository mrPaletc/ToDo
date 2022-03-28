using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Role role { get; set; }
        public IEnumerable<Job> listOfTasks { get; set; }

        public Person()
        {

        }
    }
    public enum Role
    {
        Administrator = 1,
        Director = 2,
        Worker = 3
    }
}
