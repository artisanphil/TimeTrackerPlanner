using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTrackerPlanerMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrackerPlanerMVC.Controllers
{
    public class AddTaskController : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;
        public IEnumerable<plannedTasksDetail> plannedTasks { get; set; }

        public AddTaskController(TimeTrackerPlanerMVC.Models.TasksContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            plannedTasks = from myPlans in _context.TasksPlanned
                           join myTasks in _context.TaskNames on myPlans.taskid equals myTasks.taskid
                           where myPlans.planneddate >= DateTimeExtensions.FirstDayOfWeek(DateTime.Now)
                           orderby myPlans.planneddate descending
                           select new plannedTasksDetail { 
                                weekPlanned = myPlans.planneddate,
                                taskname = myTasks.taskname,
                                estimation = myPlans.estimation
                            };

            List<SelectListItem> ProjectList =(from p in _context.Projects.AsEnumerable()
                         select new SelectListItem
                         {
                            Value = p.projectid.ToString(),
                            Text = p.projectname
                         }).ToList();

            ProjectList.Insert(0, new SelectListItem()
            {
                Text = "Select Project",
                Value = "0"
            });


            List<SelectListItem> WeekList = new List<SelectListItem>();
            WeekList.Add(new SelectListItem() { Text = "This Week", Value = "0" });
            WeekList.Add(new SelectListItem() { Text = "Next Week", Value = "1" });
            WeekList.Add(new SelectListItem() { Text = "2 Weeks", Value = "2" });

            ViewData["ProjectList"] = ProjectList;
            Console.WriteLine(plannedTasks);
            ViewData["plannedTasks"] = plannedTasks.ToList();
            ViewData["WeekList"] = new SelectList(WeekList, "Value", "Text");;

            return View();
        }

        [HttpPost]
        public List<SelectListItem> GetCategoriesByProjectId(int projectid)
        {
            List<SelectListItem> CategoryList = (from p in _context.Categories.AsEnumerable()
                                                 where p.projectid == projectid
                                                 select new SelectListItem
                                                 {
                                                     Value = p.catid.ToString(),
                                                     Text = p.catname
                                                 }
                                                ).ToList();

            CategoryList.Insert(0, new SelectListItem()
            {
                Text = "Select Category",
                Value = "0"
            });

            return CategoryList;
        }

        [HttpPost]
        public List<SelectListItem> GetTasksByCategoryId(int categoryid)
        {
            List<SelectListItem> TaskList = (from p in _context.TaskNames.AsEnumerable()
                                                 where p.categoryid == categoryid
                                                 select new SelectListItem
                                                 {
                                                     Value = p.taskid.ToString(),
                                                     Text = p.taskname
                                                 }
                                                ).ToList();

            TaskList.Insert(0, new SelectListItem()
            {
                Text = "Select Task",
                Value = "0"
            });

            return TaskList;
        }


        [HttpPost]
        public IActionResult Index(int TaskList, int TimeEstimation, int WeekList)
        {
            DateTime weekDate = DateTime.Now;
            if(WeekList >= 1)
            {
                weekDate = DateTime.Now.AddDays(WeekList * 7);
            }
            DateTime datePlanned = DateTimeExtensions.FirstDayOfWeek(weekDate);


            var tasksPlannedEntity = new TasksPlanned() { taskid = TaskList, estimation = TimeEstimation, planneddate = datePlanned };
            _context.TasksPlanned.Add(tasksPlannedEntity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public string AddProject(string item)
        {
            var projectEntity = new Projects() { projectname = item };
            _context.Projects.Add(projectEntity);
            _context.SaveChanges();

            return projectEntity.projectid.ToString();
        }

        public string AddCategory(string item, int parentid)
        {
            var categoryEntity = new Categories() { catname = item, projectid = parentid };
            _context.Categories.Add(categoryEntity);
            _context.SaveChanges();

            return categoryEntity.catid.ToString();
        }

        public string AddTask(string item, int parentid)
        {
            var taskEntity = new TaskNames() { taskname = item, categoryid = parentid };
            _context.TaskNames.Add(taskEntity);
            _context.SaveChanges();

            return taskEntity.taskid.ToString();
        }

    }
}
