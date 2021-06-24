using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace onshop.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderDetail> orderDetails { get; set; }
    }
}
