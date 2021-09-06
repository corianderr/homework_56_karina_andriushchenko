using homework_56.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_56.Controllers
{
    public class TasksController : Controller
    {
        private MobileContext _context;

        public TasksController(MobileContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                if (task != null)
                {
                    task.Status = "new";
                    _context.Tasks.Add(task);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Details(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            return View(task);
        }
        public IActionResult Open(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            task.OpenDate = DateTime.Now;
            task.Status = "opened";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Close(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            task.CloseDate = DateTime.Now;
            task.Status = "closed";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
