using System;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackerPlanerMVC.Models
{
    public class TasksContext : DbContext
    {        
        public TasksContext(DbContextOptions<TasksContext> options)
                : base(options)
        {
        }

        public DbSet<TasksDoing> TasksDoing { get; set; }    
        public DbSet<TasksPlanned> TasksPlanned { get; set; }    
        public DbSet<TaskNames> TaskNames { get; set; }    
        public DbSet<Categories> Categories { get; set; }    
        public DbSet<Projects> Projects { get; set; }    
    }
}
