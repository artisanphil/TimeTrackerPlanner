using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerAPI.Models
{
    public class TasksDoing
    {
        [Key]
        public int workid { get; set; }
        public int planid { get; set;  }
        public DateTime starttime { get; set; }
        public int duration { get; set; }
    }

    public class allCompletedTasks
    {
        public TasksDoing[] taskDone { get; set; }
    }

}
