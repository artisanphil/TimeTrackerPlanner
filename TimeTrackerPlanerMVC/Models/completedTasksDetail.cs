using System;
using System.Collections.Generic;

namespace TimeTrackerPlanerMVC.Models
{
    public class completedTasksDetail
    {
        public int workid { get; set; }
        public int planid { get; set; }
        public DateTime starttime { get; set; }
        public string taskdescription { get; set; }
        public string projectname { get; set; }
        public string categoryname { get; set; }
        public int duration { get; set; }
    }

    public class completedTasksViewModel
    {
        public List<completedTasksDetail> completedTasks { get; set; }
    }
}
