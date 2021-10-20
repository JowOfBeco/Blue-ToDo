using System;
using System.ComponentModel.DataAnnotations;

namespace BlueToDo.Models
{
    public class TodoModel
    {
        [Key]
        public int IdTask { get; set; }
        public string TaskName { get; set; }
        public string Task { get; set; }
        public DateTime TaskDate { get; set; }
        public DateTime DeadLine { get; set; }
        public PriorityTypes Priority { get; set; }
        public Users User { get; set; }
        public bool Status { get; set; }

    }

}
