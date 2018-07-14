using System.ComponentModel.DataAnnotations;

namespace TimeTrackerPlanerWeb.Models
{
    public class Projects
    {
        [Key]
        public int projectid { get; set; }
        public string projectname { get; set; }
    }
}
