using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Calculators;
using BillingSystem.Models;
using BillingSystem.Persistence;

namespace BillingSystem.Services
{
    internal class InvoiceService
    {
        private Invoice? _currentInvoice;
        private List<Invoice> _invoices;
        private InvoiceCalculator _invoiceCalculator;
        private ProductDetailCalculator _productDetailCalculator;
        private InvoicePersistence _invoicePersistence;
        public InvoiceService()
        {
            _invoicePersistence = new InvoicePersistence();
            _invoiceCalculator = new InvoiceCalculator();
            _productDetailCalculator = new ProductDetailCalculator();
            _invoices = _invoicePersistence.Load();
        }

        public void Save()
        {
            if (_currentInvoice is null)
                throw new InvalidOperationException("No hay ninguna factura activa actualmente.");
            if (_currentInvoice.Products.Count < 1)
                throw new InvalidOperationException("No se puede guardar una factura sin productos.");
            
            _productDetailCalculator.CalculateInvoiceDetails(_currentInvoice);

            _invoiceCalculator.CalculateInvoice(_currentInvoice);
            _currentInvoice.Id = _invoiceCalculator.NextId(_invoices);
            _currentInvoice.Date = DateTime.Now;

            _invoices.Add(_currentInvoice);
            _invoicePersistence.Save(_invoices);
        }
        public void NewInvoice()
        {
            _currentInvoice = new Invoice();
        }
        public void AddProductToInvoice(ProductDetail productDetail)
        {
            if (_currentInvoice is null)
                throw new InvalidOperationException("No hay ninguna factura activa actualmente.");
            if (productDetail is null)
                throw new ArgumentException(nameof(productDetail),"El detalle del producto no puede ser null");
            // en caso de que ya exista el producto, solo se suma la cantidad a la existente
            foreach (var item in _currentInvoice!.Products)
            {
                if(item.Product == productDetail.Product)
                {
                    item.Quantity += productDetail.Quantity;
                    return;
                }
            }

            _currentInvoice!.AddProduct(productDetail);
        }

        public void RemoveProductFromInvoice(int id)
        {
            if (_currentInvoice is null)
                throw new InvalidOperationException("No hay ninguna factura activa actualmente.");
            if(id < 0 || id >= _currentInvoice.Products.Count) 
                throw new ArgumentOutOfRangeException(nameof(id),"Id de producto invalido.");
            
            _currentInvoice.DeleteProduct(id);
        }

        public List<ProductDetail> GetInvoiceProducts()
        {
            return _currentInvoice?.Products ?? new List<ProductDetail>();
        }

        public List<Invoice> GetInvoices()
        {
            return _invoices ?? new List<Invoice>();
        }
    }
}
