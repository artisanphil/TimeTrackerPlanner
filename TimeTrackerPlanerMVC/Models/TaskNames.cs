using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerMVC.Models
{
    public class TaskNames
    {
        [Key]
        public int taskid { get; set; }
        public string taskname { get; set; }
        public int categoryid { get; set; }
    }
}
