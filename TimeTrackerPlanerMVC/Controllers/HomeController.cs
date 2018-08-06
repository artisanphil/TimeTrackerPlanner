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
                           orderby myTasks.taskname ascending
                            select new SelectListItem
                            {
                                Value = myPlans.taskid.ToString(),
                                Text = myTasks.taskname
                           }).ToList();           

            var completedTasks = GetCompletedTasksViewModel();

            ViewData["plannedTasksList"] = plannedTasksWeek;

            return View(completedTasks);
        }

        [HttpPost]
        public IActionResult Index(int plannedTasksList, string starttime, int duration = 0, int workid = 0)
        {
            TimeSpan ts = TimeSpan.Parse(starttime);
            DateTime startDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ts.Hours, ts.Minutes, 0); 

            if (duration == 0)
            {
                TimeSpan timedifference = DateTime.Now - startDateTime;
                duration = (int)timedifference.TotalMinutes;
            }

            if (workid > 0)
            {
                var tasksDoingEntity = _context.TasksDoing.Find(workid);
                tasksDoingEntity.duration = duration;
            }
            else
            {
                var tasksDoingEntity = new TasksDoing() { taskid = plannedTasksList, starttime = startDateTime, duration = duration };
                _context.TasksDoing.Add(tasksDoingEntity);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCompletedTasks()
        {

            var completedTasks = GetCompletedTasksViewModel();

            return PartialView("CategoryResults", completedTasks);
        }

        private completedTasksViewModel GetCompletedTasksViewModel()
        {
            completedTasksViewModel completedTasksVM = new completedTasksViewModel();

            completedTasksVM.completedTasks = (from tasksCompleted in _context.TasksDoing
                                         join myTasks in _context.TaskNames on tasksCompleted.taskid equals myTasks.taskid
                                               where tasksCompleted.starttime >= DateTime.Today && tasksCompleted.starttime <= DateTime.Today.AddDays(1)
                                         orderby tasksCompleted.starttime ascending
                                         select new completedTasksDetail
                                         {
                                             starttime = tasksCompleted.starttime,
                                             taskname = myTasks.taskname,
                                             duration = tasksCompleted.duration
                                         }).ToList();

            return completedTasksVM;
        }

        public string Start(int taskid)
        {
            var workEntity = new TasksDoing() { taskid = taskid, starttime = DateTime.Now };
            _context.TasksDoing.Add(workEntity);
            _context.SaveChanges();

            return workEntity.workid.ToString();
        }
        /*
        public int Stop(int workid, int duration)
        {
            var workEntity = _context.TasksDoing.Find(workid);
            workEntity.duration = duration;
            _context.SaveChanges();

            return 1;
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
