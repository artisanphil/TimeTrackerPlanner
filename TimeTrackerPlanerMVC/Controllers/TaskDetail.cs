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
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetail : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;

        public TaskDetail(TimeTrackerPlanerMVC.Models.TasksContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public TasksPlanned Get(int id = 0)
        {
            var TaskDetail = _context.TasksPlanned.Find(id);

            TaskDetail.projectid = _context.Categories.Find(TaskDetail.catid).projectid;

            Console.WriteLine("#TaskDetail#");
            Console.WriteLine(TaskDetail);

            return TaskDetail;
        }

    }
}
