﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    /// <summary>
    /// This class represents the products table.
    /// @Author: ANHDVHE151359
    /// </summary>
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public override string ToString()
        {
            return ProductId + " " + ProductName+" " +Weight;
        }
    }
}
