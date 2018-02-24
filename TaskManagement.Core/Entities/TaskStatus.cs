using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class TaskStatus
    {
        [Key]
        [Required]
        public short TaskStatusId { get; set; }
        [Required]
        [MaxLength(20)]
        public string TaskStatusName { get; set; }

        ICollection<UserTask> Tasks { get; set; }
    }
}
