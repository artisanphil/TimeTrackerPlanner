using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerMVC.Models
{
    public class TasksPlanned
    {
        [Key]
        public int planid { get; set; }
        public int taskid { get; set; }

        public virtual TasksDoing TasksDoing { get; set; }
    }
}
