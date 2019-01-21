using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerPlanerAPI.Models;

namespace TimeTrackerPlanerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannedTasksController : ControllerBase
    {
        private readonly TimeTrackerPlanerAPI.Models.TasksContext _context;

        public PlannedTasksController(TimeTrackerPlanerAPI.Models.TasksContext context)
        {
            _context = context;
        }


        // GET api/values
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        public IEnumerable<PlannedTasks>  Get()
        {
            PlannedTasks plannedTasks = new PlannedTasks();
            var myPlannedTasks = plannedTasks.GetPlannedTasks();
            //return new string[] { myPlannedTasks.ToString() };
            return myPlannedTasks;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post([FromBody] allCompletedTasks values)
        {
            //Console.WriteLine("RECEIVED planid: " + values.taskDone[0].planid);

            _context.TasksDoing.Add(values.taskDone[0]);

            _context.SaveChanges();

            return Ok(values);
        }
      
    }
}
