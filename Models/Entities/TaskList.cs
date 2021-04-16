using System.Collections.Generic;

namespace ToDo.Models.Entities
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ATask> Tasks { get; set; }
    }
}