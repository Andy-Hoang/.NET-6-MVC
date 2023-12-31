﻿using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
		[MaxLength(30)]
        public string Name { get; set; }
        [Display(Name = "Display Order")]
        [Range(1,100, ErrorMessage = "Display Order must be within 1-100 only!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
