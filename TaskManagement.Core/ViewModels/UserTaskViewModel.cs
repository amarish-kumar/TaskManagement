using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.ViewModels
{
    public class CreateUserTaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime DueDate { get; set; }
        public Guid AssignedTo { get; set; }
        public Guid CreatedBy { get; set; }
        public string Status { get; set; }
    }

    public class UserTaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string AssignedToUserName { get; set; }
        public string CreatedByUserName { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
    }
}
