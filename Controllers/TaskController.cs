using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDo.Data;
using ToDo.Models.Entities;

namespace ToDo.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return View(tasks);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed([Bind("IsDone,Deadline,Description,Name")] ATask task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return View(task);
        }
        
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditTask(int? id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (await TryUpdateModelAsync(task, "", s => s.Name, s => s.Description, s => s.Deadline, s => s.IsDone))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        
        [HttpGet, ActionName("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return View(task);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}