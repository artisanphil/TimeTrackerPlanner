using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTrackerPlanerWeb.Models;

namespace TimeTrackerPlanerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TasksContext _db;

        public IndexModel(TasksContext db)
        {
            _db = db;
        }

        public IList<TasksDoing> tasks { get;set; }

		public async Task OnGetAsync()
        {
            Console.WriteLine("Starting...");
            tasks = await _db.TasksDoing.ToListAsync();

        }
    }
}
