using System;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackerPlanerMVC.Models;

namespace TimeTrackerPlanerMVC.Services
{
    public class TaskService
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;

        public TaskService(TimeTrackerPlanerMVC.Models.TasksContext context)
        {
            _context = context;
        }

        public completedTasksViewModel GetCompletedTaskItems()
        {
            completedTasksViewModel completedTasksVM = new completedTasksViewModel();

            completedTasksVM.completedTasks = (from tasksCompleted in _context.TasksDoing
                                               join myPlans in _context.TasksPlanned on tasksCompleted.planid equals myPlans.planid
                                           join myCategories in _context.Categories on myPlans.catid equals myCategories.catid
                                           join myProjects in _context.Projects on myCategories.projectid equals myProjects.projectid
                                           where tasksCompleted.starttime >= DateTime.Today && tasksCompleted.starttime <= DateTime.Today.AddDays(1)
                                           orderby tasksCompleted.starttime ascending
                                           select new completedTasksDetail
                                           {
                                               workid = tasksCompleted.workid,
                                               planid = tasksCompleted.planid,
                                               starttime = tasksCompleted.starttime,
                                               categoryname = myCategories.catname,
                                               projectname = myProjects.projectname,
                                               taskdescription = (myPlans.taskdescription.Length > 30) ? string.Concat(myPlans.taskdescription.Substring(0, 30), "...") : myPlans.taskdescription,
                                               duration = tasksCompleted.duration
                                           }).ToList();

            Console.WriteLine(completedTasksVM);

            return completedTasksVM;

        }
    }
}
