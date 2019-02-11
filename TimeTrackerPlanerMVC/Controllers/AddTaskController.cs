using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult Index(int planid = 0)
        {
            //Console.WriteLine("USERID: " + this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //Console.WriteLine("USER:" + User.Identity.Name);

            plannedTasks = from myPlans in _context.TasksPlanned
                            join myCategories in _context.Categories on myPlans.catid equals myCategories.catid
                           join myProjects in _context.Projects on myCategories.projectid equals myProjects.projectid
                            where myPlans.planneddate >= DateTimeExtensions.FirstDayOfWeek(DateTime.Now) 
                           orderby myPlans.planneddate descending, myProjects.projectname ascending
                           select new plannedTasksDetail { 
                                planid = myPlans.planid,
                                weekPlanned = myPlans.planneddate,
                                taskdescription = (myPlans.taskdescription.Length > 50) ? string.Concat(myPlans.taskdescription.Substring(0, 50), "...") : myPlans.taskdescription,
                                catname = myCategories.catname,
                                projectname = myProjects.projectname,
                                estimation = myPlans.estimation,
                                timeSpent = (from p in _context.TasksDoing
                                             where p.planid == myPlans.planid &&
                                             p.starttime >=  DateTimeExtensions.FirstDayOfWeek(myPlans.planneddate) &&
                                             p.starttime <= DateTimeExtensions.FirstDayOfWeek(myPlans.planneddate.AddDays(7))
                                    select p.duration).Sum(),
                                completed = myPlans.completed
                            };

            List<SelectListItem> ProjectList =(from p in _context.Projects.AsEnumerable()
                         orderby p.projectname ascending
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
        public IActionResult Index(int CategoryList, string TaskDescription, int TimeEstimation, int WeekList)
        {
            DateTime weekDate = DateTime.Now;
            if(WeekList >= 1)
            {
                weekDate = DateTime.Now.AddDays(WeekList * 7);
            }
            DateTime datePlanned = DateTimeExtensions.FirstDayOfWeek(weekDate);


            var tasksPlannedEntity = new TasksPlanned() { catid = CategoryList, taskdescription = TaskDescription, estimation = TimeEstimation, planneddate = datePlanned };
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

        public int setCompleted(int planid, bool completed)
        {
            var taskEntity = _context.TasksPlanned.Find(planid);
            taskEntity.completed = false;
            if(!completed)
            {
                taskEntity.completed = true;    
            }
            _context.SaveChanges();

            return planid;
        }


    }
}
