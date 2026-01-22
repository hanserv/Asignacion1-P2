using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Models
{
    internal class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Itbis Tax { get; set; }

        public Product(string name, string description, double price, Itbis tax)
        {
            Name = name;
            Description = description;
            Price = price;
            Tax = tax;
        }
    }
}
