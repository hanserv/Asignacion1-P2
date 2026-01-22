using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BillingSystem.Models;

namespace BillingSystem.Persistence
{
    internal class ProductPersistence : IPersistence<Product>
    {
        private readonly string _path = "products.json";
        public void Save(List<Product> products)
        {
            var jsonOption = new JsonSerializerOptions() { WriteIndented=true };
            var jsonString = JsonSerializer.Serialize<List<Product>>(products,jsonOption);
            File.WriteAllText(_path, jsonString);
        }
        public List<Product> Load()
        {
            if (!File.Exists(_path))
                return new List<Product>();

            var jsonProducts = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Product>>(jsonProducts)
                ?? new List<Product>();
        }
    }
}