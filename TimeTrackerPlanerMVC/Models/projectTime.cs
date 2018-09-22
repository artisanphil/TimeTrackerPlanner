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
            string sql = @"SELECT Projects.projectid, Projects.projectname, SUM(TasksDoing.duration) AS timeSpent 
            FROM TasksPlanned 
            INNER JOIN TasksDoing ON TasksDoing.planid = TasksPlanned.planid
            INNER JOIN TaskNames ON TaskNames.taskid = TasksPlanned.taskid
            INNER JOIN Categories ON Categories.catid = TaskNames.categoryid
            INNER JOIN Projects ON Projects.projectid = Categories.projectid
            WHERE TasksDoing.starttime >= '{0}' AND TasksDoing.starttime <= '{1}'
            GROUP BY Projects.projectid";

            DateTime dateFromDate = (DateTime)dateFrom;
            DateTime dateUntilDate = (DateTime)dateUntil;
            string sqlQuery = string.Format(sql, dateFromDate.ToString("yyyy-MM-dd 00:00:00"), dateUntilDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

            return projectTimeSpent.FromSql(sqlQuery).ToList();
        }
    }
}
