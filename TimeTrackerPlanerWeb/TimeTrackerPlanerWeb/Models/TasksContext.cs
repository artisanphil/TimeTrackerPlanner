using System;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackerPlanerWeb.Models
{
    public class TasksContext : DbContext
    {        
        public TasksContext(DbContextOptions<TasksContext> options)
                : base(options)
        {
        }

        public DbSet<TasksDoing> TasksDoing { get; set; }    
        public DbSet<TasksPlanned> TasksPlanned { get; set; }    
    }
}
