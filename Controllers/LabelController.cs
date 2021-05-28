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
    public class LabelController : BaseController
    {
        private readonly TaskContext _context;

        public LabelController(TaskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
            var labels = _context.Labels.Where(x => x.User == user);

            return View(await labels.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed([Bind("Name, Color")]Label label)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
                label.User = user;
                _context.Add(label);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }
            return View(label);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
            {
                return RedirectToAction("Index");
            }
            
            if (!await IsAuthenticated(id ?? default(int)))
            {
                return Unauthorized();
            }
            
            return View(label);
        }
        
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(int id)
        {
            if (!await IsAuthenticated(id))
            {
                return Unauthorized();
            }
            
            var label = await _context.Labels.FirstOrDefaultAsync(x => x.Id == id);
            
            if (await TryUpdateModelAsync(label, "", s => s.Name, s => s.Color))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            if (!await IsAuthenticated(id))
            {
                return Unauthorized();
            }
            
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
            {
                return RedirectToAction("Index");
            }
            
            _context.Labels.Remove(label);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
        
        public async Task<bool> IsAuthenticated(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == User.Identity.Name);
            var list = await _context.Labels.FindAsync(id);
            return list.User == user;
        }
    }
}