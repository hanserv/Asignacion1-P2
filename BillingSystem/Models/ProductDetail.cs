using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Models
{
    internal class ProductDetail
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public double Taxes { get; set; }
        public double SubTotal { get; set; }

        public ProductDetail() { }
        public ProductDetail(Product product,int quantity) 
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
