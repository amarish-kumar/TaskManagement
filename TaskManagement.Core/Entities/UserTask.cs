using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class UserTask
    {
        [Key]
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        [MaxLength(128)]
        [ForeignKey("AssignedToUser")]
        public string AssignedToUser_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string TaskTitle { get; set; }
        [Required]
        [MaxLength(100)]
        public string TaskDesc { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public short TaskStatusId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [MaxLength(128)]
        [ForeignKey("CreatedByUser")]
        public string CreatedByUser_Id { get; set; }

        public DateTime? DateUpdated { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual TaskStatus TaskStatus { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }
    }
}
