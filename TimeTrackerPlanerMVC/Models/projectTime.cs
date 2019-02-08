using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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

        public List<projectTime> GetProjectsTimeSpent(DateTime? dateFrom, DateTime? dateUntil)
        {
            DateTime dateFromDate = (DateTime)dateFrom;
            DateTime dateUntilDate = (DateTime)dateUntil;
            var starttime = dateFromDate.ToString("yyyy-MM-dd 00:00:00");
            var endtime = dateUntilDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

            string sql = $@"SELECT Projects.projectid, Projects.projectname, SUM(TasksDoing.duration) AS timeSpent 
            FROM TasksPlanned 
            INNER JOIN TasksDoing ON TasksDoing.planid = TasksPlanned.planid
            INNER JOIN Categories ON Categories.catid = TasksPlanned.catid
            INNER JOIN Projects ON Projects.projectid = Categories.projectid
            WHERE TasksDoing.starttime >= '{starttime}' AND TasksDoing.starttime <= '{endtime}'
            GROUP BY Projects.projectid";

            return projectTimeSpent.FromSql(sql).ToList();
        }
    }
}
