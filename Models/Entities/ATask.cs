using System;

namespace ToDo.Models.Entities
{
    public class ATask
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}