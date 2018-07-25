using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTrackerPlanerMVC.Models
{
    public class TaskNames
    {
        [Key]
        public int taskid { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string taskname { get; set; }
        public int categoryid { get; set; }
    }
}
