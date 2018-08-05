using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTrackerPlanerMVC.Models;

namespace TimeTrackerPlanerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;

        public HomeController(TimeTrackerPlanerMVC.Models.TasksContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<SelectListItem> plannedTasksWeek = (from myPlans in _context.TasksPlanned
                           join myTasks in _context.TaskNames on myPlans.taskid equals myTasks.taskid
                           where myPlans.planneddate == DateTimeExtensions.FirstDayOfWeek(DateTime.Now)
                           orderby myTasks.taskname descending
                            select new SelectListItem
                            {
                                Value = myPlans.taskid.ToString(),
                                Text = myTasks.taskname
                           }).ToList();           

            ViewData["plannedTasksList"] = plannedTasksWeek;

            return View();
        }

        [HttpPost]
        public IActionResult Index(int plannedTasksList, string starttime, int duration)
        {
            TimeSpan ts = TimeSpan.Parse(starttime);
            DateTime startDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ts.Hours, ts.Minutes, 0); 

            var tasksDoingEntity = new TasksDoing() { taskid = plannedTasksList, starttime = startDateTime, duration = duration };
            _context.TasksDoing.Add(tasksDoingEntity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public string Start(int taskid)
        {
            var workEntity = new TasksDoing() { taskid = taskid, starttime = DateTime.Now };
            _context.TasksDoing.Add(workEntity);
            _context.SaveChanges();

            return workEntity.workid.ToString();
        }

        public int Stop(int workid, int duration)
        {
            var workEntity = _context.TasksDoing.Find(workid);
            workEntity.duration = duration;
            _context.SaveChanges();

            return 1;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
