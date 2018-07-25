using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTrackerPlanerMVC.Models
{
    public class Categories
    {
        [Key]
        public int catid { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string catname { get; set; }
        public int projectid { get; set; }
    }
}
