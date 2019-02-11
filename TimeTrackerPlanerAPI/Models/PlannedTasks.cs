using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace TimeTrackerPlanerAPI.Models
{
    public class PlannedTasks
    {
        public int planid { get; set; }
        public string taskname { get; set; }
        public string projectname { get; set;  }
        public string catname { get; set;  }

        public IEnumerable<PlannedTasks> GetPlannedTasks()
        {
            var plannedTasks = new PlannedTasks();

            using (MySqlConnection sqlConnection = new MySqlConnection(Startup.ConnectionString))
            {
                sqlConnection.Open();
                using (MySqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT TasksPlanned.planid, taskdescription, projectname, catname
                                FROM TasksPlanned
                                INNER JOIN Categories ON Categories.catid = TasksPlanned.catid
                                INNER JOIN Projects ON Projects.projectid = Categories.projectid
                                WHERE planneddate >= DATE(CURRENT_DATE - INTERVAL (DAYOFWEEK(CURRENT_DATE) - 1) DAY)
                                ORDER BY projectname ASC";
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plannedTasks.planid = (int) reader["planid"];
                            plannedTasks.taskname = reader["taskdescription"].ToString();
                            plannedTasks.projectname = reader["projectname"].ToString();
                            plannedTasks.catname = reader["catname"].ToString();

                            yield return plannedTasks;
                        }
                    }
                }
            }
        }
    }
}
