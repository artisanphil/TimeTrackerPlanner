using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeTrackerPlanerWeb.Models;

namespace TimeTrackerPlanerWeb.Pages
{
    public class AddTaskModel : PageModel
    {
        [BindProperty]
        public Projects Projects { get; set; }

        public void OnGet()
        {
            Console.WriteLine("OnGet....");
        }

        public IActionResult OnPost()
        {
            return Content($"Hello {Projects.projectname}");
            //return Page();
        }  
    }
}
