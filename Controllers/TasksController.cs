using homework_56.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
                if (task != null)
                    return View(task);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var task = new Models.Task { Id = id.Value };
                _context.Entry(task).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }
}
