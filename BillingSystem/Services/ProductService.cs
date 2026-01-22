using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Models;
using BillingSystem.Persistence;

namespace BillingSystem.Services
{
    internal class ProductService
    {
        private List<Product> _products;
        private IPersistence<Product> _productPersistence;
        public ProductService()
        {
            _productPersistence = new ProductPersistence();
            _products = _productPersistence.Load();
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
        public void AddProduct(Product product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product),"El producto no puede ser null.");

            // Evitar productos duplicados
            foreach(var item in _products)
            {
                if (product.Name.ToLower() == item.Name.ToLower())
                    throw new InvalidOperationException($"El producto {product.Name} ya existe.");
            }

            _products.Add(product);
            _productPersistence.Save(_products);
        }

        public void DeleteProduct(int id)
        {
            if (id < 0 || id >= _products.Count)
                throw new ArgumentOutOfRangeException(nameof(id),"Id de producto invalido.");
            
            _products.RemoveAt(id);
            _productPersistence.Save(_products);
        }
    }
}
