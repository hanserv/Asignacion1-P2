using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Services;

namespace BillingSystem.Models
{
    internal class Invoice
    {
        public int Id { get; set; }
        public List<ProductDetail> Products { get; set; }
        public DateTime Date { get; set; }
        public double SubTotal { get; set; }
        public double Taxes { get; set; }
        public double Total { get; set; }

        private ProductService _productService;

        public Invoice()
        {
            Products = new List<ProductDetail>();
            _productService = new ProductService();
        }

        public void AddProduct(ProductDetail productDetail)
        {
            Products.Add(productDetail);
        }
        public void DeleteProduct(int id)
        {
            Products.RemoveAt(id);
        }
    }
}
