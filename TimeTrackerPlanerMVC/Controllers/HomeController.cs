using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTrackerPlanerMVC.Models;
using TimeTrackerPlanerMVC.Services;

namespace TimeTrackerPlanerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;
        private readonly TaskService taskService;

        public HomeController(TimeTrackerPlanerMVC.Models.TasksContext context, TaskService taskService)
        {
            _context = context;
            this.taskService = taskService;
        }

        public IActionResult Index(int projectid = 0, int taskid = 0)
        {
            ViewData["plannedTasksList"] = GetPlannedTasksByProjectId(taskid);

            var completedTasks = taskService.GetCompletedTaskItems();


            return View(completedTasks);
        }

        [HttpPost]
        public IActionResult Index(int ProjectList, int plannedTasksList, string starttime, int duration = 0, int workid = 0)
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

            return RedirectToAction("Index", new { projectid = ProjectList, taskid = plannedTasksList  });
        }

        [HttpGet]
        public IActionResult GetCompletedTasks()
        {

            var completedTasks = taskService.GetCompletedTaskItems();

            return PartialView("CategoryResults", completedTasks);
        }

        public string Start(int taskid)
        {
            var workEntity = new TasksDoing() { taskid = taskid, starttime = DateTime.Now };
            _context.TasksDoing.Add(workEntity);
            _context.SaveChanges();

            return workEntity.workid.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public List<SelectListItem> GetPlannedTasksByProjectId(int taskid = 0)
        {
            List<SelectListItem> plannedTasksWeek = (from myPlans in _context.TasksPlanned
                           join myTasks in _context.TaskNames on myPlans.taskid equals myTasks.taskid
                             join myCategories in _context.Categories on myTasks.categoryid equals myCategories.catid
                             join myProjects in _context.Projects on myCategories.projectid equals myProjects.projectid
                           where myPlans.planneddate == DateTimeExtensions.FirstDayOfWeek(DateTime.Now) 
                                                     && myPlans.completed == false
                            orderby myProjects.projectname ascending, myTasks.taskname ascending
                            select new SelectListItem
                            {
                                Value = myPlans.taskid.ToString(),
                Text = myProjects.projectname + ": " + myTasks.taskname + " (" + myCategories.catname + ")" 
                           }).ToList();

            plannedTasksWeek.Insert(0, new SelectListItem()
            {
                Text = "",
                Value = "0"
            });

            if (taskid > 0)
            {
                var selected = plannedTasksWeek.Where(x => x.Value == taskid.ToString()).First();
                selected.Selected = true;
            }

            return plannedTasksWeek;
        }
    }
}
