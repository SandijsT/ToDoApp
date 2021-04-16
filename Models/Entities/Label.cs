using System.Drawing;

namespace ToDo.Models.Entities
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
        public User User { get; set; }
    }
}