using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackerPlanerMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrackerPlanerMVC.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly TimeTrackerPlanerMVC.Models.ProjectContext _context;

        public StatisticsController(TimeTrackerPlanerMVC.Models.ProjectContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(DateTime? dateFrom = null, DateTime? dateUntil = null)
        {
            dateFrom = dateFrom ?? DateTime.Now.AddDays(-7);
            dateUntil = dateUntil ?? DateTime.Now;

            var thisWeeksProjects = _context.GetProjectsTimeSpent(dateFrom, dateUntil).OrderByDescending(t => t.timeSpent).ToList();
            var totalTime = thisWeeksProjects.Sum(t => t.timeSpent);

            DateTime dateFromDate = (DateTime) dateFrom;
            ViewData["dateFrom"] = dateFromDate.ToString("yyyy-MM-dd");
            DateTime dateUntilDate = (DateTime)dateUntil;
            ViewData["dateUntil"] = dateUntilDate.ToString("yyyy-MM-dd");

            ViewData["thisWeeksProjects"] = thisWeeksProjects;
            ViewData["totalTime"] = totalTime;

            return View();
        }

        [HttpPost]
        public ActionResult Index(DateTime dateFrom, DateTime dateUntil)
        {
            return RedirectToAction("Index", new { dateFrom, dateUntil });
        }
    }
}
