using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onshop.Models
{
    public class ProductTypes
    {
        [Key]
        public int Pid { get; set; }
        [Required]
        [Display(Name ="Product Type")]
        public string ProductType { get; set; }
    }
}
