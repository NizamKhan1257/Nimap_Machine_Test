using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Display(Name ="Product Name")]
        [Required (ErrorMessage ="Product Name Is Required")]
        public string? ProductName { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category Is Required")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        
        public string? CategoryName { get; set; }
    }
}
