using homework_56.Enums;
using homework_56.Models;
using homework_56.ViewModels;
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
        public IActionResult Index(string name, DateTime? dateFrom, DateTime? dateTo, string priority, string status, string description, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Models.Task> users = _context.Tasks;
            ViewBag.NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.PrioritySort = sortOrder == SortState.PriorityAsc ? SortState.PriorityDesc : SortState.PriorityAsc;
            ViewBag.StatusSort = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewBag.CreationDateSort = sortOrder == SortState.CreationDateAsc ? SortState.CreationDateDesc : SortState.CreationDateAsc;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }
            if (dateFrom != null)
            {
                users = users.Where(p => p.CreationDate > dateFrom);
            }
            if (dateTo != null)
            {
                users = users.Where(p => p.CreationDate < dateTo);
            }
            if (!String.IsNullOrEmpty(description))
            {
                users = users.Where(p => p.Description.Contains(description));
            }
            if (!String.IsNullOrEmpty(priority))
            {
                users = users.Where(p => p.Priority.Contains(priority));
            }
            if (!String.IsNullOrEmpty(status))
            {
                users = users.Where(p => p.Status.Contains(status));
            }
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriorityAsc:
                    users = users.OrderBy(s => s.Priority);
                    break;
                case SortState.PriorityDesc:
                    users = users.OrderByDescending(s => s.Priority);
                    break;
                case SortState.StatusAsc:
                    users = users.OrderBy(s => s.Status);
                    break;
                case SortState.StatusDesc:
                    users = users.OrderByDescending(s => s.Status);
                    break;
                case SortState.CreationDateAsc:
                    users = users.OrderBy(s => s.CreationDate);
                    break;
                case SortState.CreationDateDesc:
                    users = users.OrderByDescending(s => s.CreationDate);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            TaskListViewModel tlvm = new TaskListViewModel
            {
                Status = status,
                Priority = priority,
                DateFrom = dateFrom,
                DateTo = dateTo,
                Description = description,
                Name = name,
                Tasks = users
            };
            return View(tlvm);
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
                    task.CreationDate = DateTime.Now;
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
