using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTrackerPlanerWeb.Models;

namespace TimeTrackerPlanerWeb.Pages
{
    public class AddTaskModel : PageModel
    {
        private readonly TimeTrackerPlanerWeb.Models.TasksContext _context;

        public AddTaskModel(TimeTrackerPlanerWeb.Models.TasksContext context)
        {
            _context = context;
        }


        public SelectList ProjectList { get; set; }
        public string ProjectID { get; set;  }

        public void OnGet()
        {
            ProjectList = new SelectList(_context.Projects.ToList(), "projectid", "projectname");  

            Console.WriteLine("OnGet....");
        }

        public IActionResult OnPost(string ProjectID)
        {
            //_context.Projects.Add(ProjectID);
            //_context.SaveChanges();
            return Content($"Hello {ProjectID}");

        }  
    }
}
