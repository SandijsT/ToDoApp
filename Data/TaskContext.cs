using Microsoft.EntityFrameworkCore;
using ToDo.Models.Entities;

namespace ToDo.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            
        }
        public DbSet<ATask> Tasks { get; set; }
    }
}