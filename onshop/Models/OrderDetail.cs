using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace onshop.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }       [ForeignKey("OrderID")]
        public OrderDetail order { get; set; }
        [ForeignKey("ProductID")]
        public Products product { get; set; }
    }
}
