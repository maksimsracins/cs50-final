using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ToDoDatabaseContext _databaseContext;

        public HomeController(ToDoDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            ViewBag.Categories = _databaseContext.Categories.ToList();
            ViewBag.Statuses = _databaseContext.Statuses.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<ToDo> query = _databaseContext.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status);

            if (filters.HasCategory)
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            if (filters.HasStatus)
                query = query.Where(t => t.StatusId == filters.StatusId);
            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                    query = query.Where(t => t.DueDate < today);
                else if (filters.IsFuture)
                    query = query.Where(t => t.DueDate > today);
                else if (filters.IsToday)
                    query = query.Where(t => t.DueDate == today);

            }
            var tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(tasks);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _databaseContext.Categories.ToList();
            ViewBag.Statuses = _databaseContext.Statuses.ToList();
            var task = new ToDo { StatusId = "open" };
            return View(task);
        }
        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                _databaseContext.ToDos.Add(task);
                _databaseContext.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                ViewBag.Categories = _databaseContext.Categories.ToList();
                ViewBag.Statuses = _databaseContext.Statuses.ToList();
                return View(task);
            }
        }
        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            selected = _databaseContext.ToDos.Find(selected.Id)!;
            if (selected != null)
            {
                selected.StatusId = "closed";
                _databaseContext.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = _databaseContext.ToDos.Where(t => t.StatusId == "closed").ToList();
            foreach (var task in toDelete)
            {
                _databaseContext.ToDos.Remove(task);
            }
            _databaseContext.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }
    }
}