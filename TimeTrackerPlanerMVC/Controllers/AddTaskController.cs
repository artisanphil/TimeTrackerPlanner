using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTrackerPlanerMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrackerPlanerMVC.Controllers
{
    public class AddTask : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;

        public AddTask(TimeTrackerPlanerMVC.Models.TasksContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
           // ProjectList = new SelectList(_context.Projects.ToList(), "projectid", "projectname");  

            List<SelectListItem> ProjectList = (from p in _context.Projects.AsEnumerable()
                         select new SelectListItem
                         {
                            Value = p.projectid.ToString(),
                            Text = p.projectname
                         }).ToList();


            ViewData["ProjectList"] = ProjectList;

            return View();
        }

        [HttpPost]
        public IActionResult Index(string ProjectList)
        {
            return Content($"Hello {ProjectList}");
        }
    }
}
