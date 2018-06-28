using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerWeb.Models
{
    public class TasksPlanned
    {
        [Key]
        public int planid { get; set; }
        public int taskid { get; set; }

        public virtual TasksDoing TasksDoing { get; set; }
    }
}
