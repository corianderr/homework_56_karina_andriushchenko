using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_56.ViewModels
{
    public class TaskListViewModel
    {
        public IEnumerable<Models.Task> Tasks { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
