using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerMVC.Models
{
    public class TasksPlanned
    {
        [Key]
        public int planid { get; set; }
        public int taskid { get; set; }
        public int estimation { get; set; }
        public DateTime planneddate { get; set;  }
        public bool completed { get; set;  }
    }
}
