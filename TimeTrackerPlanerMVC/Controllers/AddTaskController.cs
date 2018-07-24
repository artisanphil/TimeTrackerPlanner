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
    public class AddTaskController : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.TasksContext _context;

        public AddTaskController(TimeTrackerPlanerMVC.Models.TasksContext context)
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

            return CategoryList;
        }

        [HttpPost]
        public IActionResult Index(string ProjectList)
        {
            return Content($"Hello {ProjectList}");
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

    }
}
