using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Api.Models
{
    public class TodoItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IsCompleted is required"), DefaultValue(false)]
        public bool IsCompleted { get; set; }

        [NotMapped]
        public string Message { get; set; }
    }
}
