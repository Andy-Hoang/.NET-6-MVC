using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		[Required]
		public string ISBN { get; set; }
		[Required]
		public string Author { get; set; }
		[Required]
		[Display(Name = "List Price")]
		[Range(1, 1000)]
		public double ListPrice { get; set; }

		[Required]
		[Display(Name = "Price for 1-50")]
		[Range(1, 1000, ErrorMessage = "Price must be within 1-1000!")]
		public double Price { get; set; }

		[Required]
		[Display(Name = "Price for 50+")]
		[Range(1, 1000, ErrorMessage = "Price must be within 1-1000!")]
		public double Price50 { get; set; }

		[Required]
		[Display(Name = "Price for 100+")]
		[Range(1, 1000, ErrorMessage = "Price must be within 1-1000!")]
		public double Price100 { get; set; }

		public int CategoryId { get; set; }			// column to actually store FK
        [ForeignKey("CategoryId")]                  // indicate use the foreignkey FK 'CategoryId' for the table 'Category'
		[ValidateNever]
		public Category Category { get; set; }      // this is a Navigation Prop to table "Category"
		[ValidateNever]
		public string ImageUrl { get; set; }
	}
}
