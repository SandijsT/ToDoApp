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
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<UrgentTaskList> UrgentTaskLists { get; set; }
    }
}