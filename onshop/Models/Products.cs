using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace onshop.Models
{
    public class Products
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal price { get; set; }
      
        public string Image { get; set; }
        [Required]
        [Display(Name ="Product Color")]
        public string ProductColour { get; set; }
        [Required]
         [Display(Name ="Available")]
        public bool IsAvailable { get; set; }
        [Required]
        [Display(Name ="Poduct Type")]
        public int ProductTypesId { get; set; }
        [ForeignKey("ProductTypesId")]
        public ProductTypes ProductTypes { get; set; }
    }
}
