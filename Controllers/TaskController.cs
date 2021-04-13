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
    [Route("api/{controller}")]
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Task()
        {
            return View( await _context.Tasks.ToListAsync());
        }
/*
        [HttpGet]
        public async Task<ActionResult<List<ATask>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ATask>> GetTask(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }*/



    }
}