using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreAndFood.Data.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Category name cannot be empty!")]
        [StringLength(30, ErrorMessage = "Please insert name between 5-20 characters.", MinimumLength = 5)]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool Status { get; set; }

        public List<Food> Foods { get; set; }
    }
}
