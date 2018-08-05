using System;
using System.Collections.Generic;

namespace TimeTrackerPlanerMVC.Models
{
    public class completedTasksDetail
    {
        public DateTime starttime { get; set; }
        public string taskname { get; set; }
        public int duration { get; set; }
    }

    public class completedTasksViewModel
    {
        public List<completedTasksDetail> completedTasks { get; set; }
    }
}
