using System;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackerPlanerAPI.Models
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options)
                : base(options)
        {
        }

        public DbSet<TasksDoing> TasksDoing { get; set; }
    }
}