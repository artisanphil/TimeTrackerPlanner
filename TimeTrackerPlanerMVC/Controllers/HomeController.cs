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
        private int projectid;

        public HomeController(TimeTrackerPlanerMVC.Models.TasksContext context)
        {
            _context = context;
        }

        public IActionResult Index(int projectid = 0, int taskid = 0)
        {
            List<SelectListItem> myProjectList = (from p in _context.Projects.AsEnumerable()
                                                orderby p.projectname ascending
                                                select new SelectListItem
                                                {
                                                    Value = p.projectid.ToString(),
                                                    Text = p.projectname
                                                }).ToList();

            myProjectList.Insert(0, new SelectListItem()
            {
                Text = "Select Project",
                Value = "0"
            });


            var selected = myProjectList.Where(x => x.Value == projectid.ToString()).First();
            selected.Selected = true;

            if(projectid > 0)
            {   
                ViewData["plannedTasksList"] = GetPlannedTasksByProjectId(projectid, taskid);
            }

            var completedTasks = GetCompletedTasksViewModel();

            ViewData["ProjectList"] = myProjectList;

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

            var completedTasks = GetCompletedTasksViewModel();

            return PartialView("CategoryResults", completedTasks);
        }

        private completedTasksViewModel GetCompletedTasksViewModel()
        {
            completedTasksViewModel completedTasksVM = new completedTasksViewModel();

            completedTasksVM.completedTasks = (from tasksCompleted in _context.TasksDoing
                                            join myTasks in _context.TaskNames on tasksCompleted.taskid equals myTasks.taskid
                                            join myCategories in _context.Categories on myTasks.categoryid equals myCategories.catid
                                            join myProjects in _context.Projects on myCategories.projectid equals myProjects.projectid
                                            where tasksCompleted.starttime >= DateTime.Today && tasksCompleted.starttime <= DateTime.Today.AddDays(1)
                                            orderby tasksCompleted.starttime ascending
                                         select new completedTasksDetail
                                         {
                                             starttime = tasksCompleted.starttime,
                                             categoryname = myCategories.catname,
                                             projectname = myProjects.projectname,
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public List<SelectListItem> GetPlannedTasksByProjectId(int projectid, int taskid = 0)
        {
            List<SelectListItem> plannedTasksWeek = (from myPlans in _context.TasksPlanned
                           join myTasks in _context.TaskNames on myPlans.taskid equals myTasks.taskid
                             join myCategories in _context.Categories on myTasks.categoryid equals myCategories.catid
                             join myProjects in _context.Projects on myCategories.projectid equals myProjects.projectid
                           where myPlans.planneddate == DateTimeExtensions.FirstDayOfWeek(DateTime.Now) && myProjects.projectid == projectid
                           orderby myTasks.taskname ascending
                            select new SelectListItem
                            {
                                Value = myPlans.taskid.ToString(),
                                Text = myTasks.taskname + " (" + myCategories.catname + ")"
                           }).ToList();

            if (taskid > 0)
            {
                var selected = plannedTasksWeek.Where(x => x.Value == taskid.ToString()).First();
                selected.Selected = true;
            }

            return plannedTasksWeek;
        }
    }
}
