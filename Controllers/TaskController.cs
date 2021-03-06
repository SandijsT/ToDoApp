using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models.Entities;

namespace ToDo.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            var lists = _context.TaskLists.Include(x => x.Tasks).AsNoTracking();
            
            var tasks = lists.Where(x => x.Id == id);
            
            return View(await tasks.ToListAsync());
        }
        
        public async Task<IActionResult> Create(int? listId)
        {
            
            if (listId == null)
            {
                return RedirectToAction("Index", "TaskList");
                
            }
            if (!await IsAuthenticated(listId ?? default(int)))
            {
                return Unauthorized();
            }
            
            var list = await _context.TaskLists.FindAsync(listId);
            var task = new ATask {TaskList = list};
            return View(task);
        }
        
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed([Bind("IsDone,Deadline,Description,Name")] ATask task, int? id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "TaskList");
            if (id == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            var list = await _context.TaskLists.FindAsync(id);
            _context.Tasks.Add(task).Entity.TaskList = list;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new {id});
        }
        
        public async Task<IActionResult> Edit(int? id, int? listId)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            
            if (!await IsAuthenticated(listId ?? default(int)))
            {
                return Unauthorized();
            }

            var list = await _context.TaskLists.FindAsync(listId);
            if (list == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            task.TaskList = list;
            
            return View(task);
        }
        
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditTask(int id, int listId)
        {
            if (!await IsAuthenticated(listId))
            {
                return Unauthorized();
            }
            
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (await TryUpdateModelAsync(task, "", s => s.Name, s => s.Description, s => s.Deadline, s => s.IsDone))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new {id = listId});
            }

            return RedirectToAction("Index", new {id = listId});
        }
        
        [HttpGet, ActionName("Details")]
        public async Task<IActionResult> Details(int? id, int? listId)
        {
            if (!await IsAuthenticated(listId ?? default(int)))
            {
                return Unauthorized();
            }
        
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            
            var list = await _context.TaskLists.FindAsync(listId);
            if (list == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            task.TaskList = list;
            
            return View(task);
        }
        
        public async Task<IActionResult> Delete(int? id, int? listId)
        {
            if (!await IsAuthenticated(listId ?? default(int)))
            {
                return Unauthorized();
            }
            
            var task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            var list = await _context.TaskLists.FindAsync(listId);
            if (list == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            task.TaskList = list;
            
            return View(task);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int listId)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new {id = listId});
        }

        public async Task<IActionResult> Labels(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return RedirectToAction("Index", "TaskList");
            }
            if (!await IsAuthenticated(task.TaskList.Id))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
            var labels = await _context.Labels.Where(x => x.User == user).ToListAsync();
            return View(labels);
        }
        
        [HttpPost]
        public async void CheckBox(int? id, bool value)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            
            if (task != null)
            {
                task.IsDone = value;
                await _context.SaveChangesAsync();
            }
            if (task == null)
            {
                RedirectToAction(nameof(Index));
            }
        }
        
        public async Task<bool> IsAuthenticated(int listId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
            var list = await _context.TaskLists.FindAsync(listId);
            return list.ListsUser == user;
        }
    }
}