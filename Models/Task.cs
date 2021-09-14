using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace homework_56.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Priority { get; set; }
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public string ExecutorId { get; set; }
        public User Executor { get; set; }
    }
}
