using System.Collections.Generic;

namespace ToDo.Models.Entities
{
    public class UrgentTaskList
    {
        public int Id { get; set; }
        public List<ATask> Tasks { get; set; }
    }
}