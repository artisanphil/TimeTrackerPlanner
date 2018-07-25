using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTrackerPlanerMVC.Models
{
    public class Projects
    {
        [Key]
        public int projectid { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string projectname { get; set; }
    }
}
