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
    }
}
