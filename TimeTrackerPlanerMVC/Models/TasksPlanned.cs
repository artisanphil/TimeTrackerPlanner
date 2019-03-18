using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTrackerPlanerMVC.Models
{
    public class TasksPlanned
    {
        [Key]
        public int planid { get; set; }
        [NotMapped]
        public int projectid { get; set; }
        public int catid { get; set; }
        public string taskdescription { get; set;  }
        public int estimation { get; set; }
        public DateTime planneddate { get; set;  }
        public int day { get; set; }
        public bool completed { get; set;  }
    } 
}
