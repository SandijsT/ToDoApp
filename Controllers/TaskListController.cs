using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models.Entities;

namespace ToDo.Controllers
{
    public class TaskListController : BaseController
    {
        private readonly TaskContext _context;

        public TaskListController(TaskContext context)
        {
            _context = context;
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var taskLists = _context.TaskLists.Include(x => x.Tasks).AsNoTracking();
            return View(await taskLists.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Name, Description")] TaskList list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Task", new { Id = id });
        }
    }
}