using System;
namespace TimeTrackerPlanerMVC.Models
{
    public class plannedTasksDetail
    {
        public DateTime weekPlanned { get; set; }
        public string taskname { get; set; }
        public string catname { get; set;  }
        public string projectname { get; set;  }
        public int estimation { get; set; }
    }
}
