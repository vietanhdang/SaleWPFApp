using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    /// <summary>
    /// This class represents the order_details table.
    /// @Author: ANHDVHE151359
    /// </summary>
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
