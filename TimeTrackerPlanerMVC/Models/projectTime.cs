using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackerPlanerMVC.Models
{
    public class projectTime
    {
        [Key]
        public int projectid { get; set; }
        public string projectname { get; set; }
        public int timeSpent { get; set;  }
    }


    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
                : base(options)
        {
        }

        public DbSet<projectTime> projectTimeSpent { get; set; }
    }
}
