using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackerPlanerMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrackerPlanerMVC.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.ProjectContext _context;

        public StatisticsController(TimeTrackerPlanerMVC.Models.ProjectContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            string sql = @"SELECT Projects.projectid, Projects.projectname, SUM(TasksDoing.duration) AS timeSpent 
            FROM TasksPlanned 
            INNER JOIN TasksDoing ON TasksDoing.planid = TasksPlanned.planid
            INNER JOIN TaskNames ON TaskNames.taskid = TasksPlanned.taskid
            INNER JOIN Categories ON Categories.catid = TaskNames.categoryid
            INNER JOIN Projects ON Projects.projectid = Categories.projectid
            WHERE TasksPlanned.planneddate >= '2018-09-16 00:00:00'
            GROUP BY Projects.projectid";

            var thisWeeksProjects = _context.projectTimeSpent.FromSql(sql).ToList();

            /*
            foreach(var project in thisWeeksProjects)
            {
                Console.WriteLine("project: " + project.projectname + " " + project.timeSpent);
            }
            */
            ViewData["thisWeeksProjects"] = thisWeeksProjects;

            return View();
        }
    }
}
