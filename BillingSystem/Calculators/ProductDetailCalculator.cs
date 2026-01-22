using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Models;

namespace BillingSystem.Calculators
{
    internal class ProductDetailCalculator
    {
        public double CalculateSubTotal(ProductDetail productDetail)
        {
            if (productDetail is null)
                throw new ArgumentNullException(nameof(productDetail), "El detalle del producto no puede ser null.");

            return productDetail!.Product!.Price * productDetail.Quantity;
        }
        public double CalculateTaxes(ProductDetail productDetail)
        {
            if (productDetail is null)
                throw new ArgumentNullException(nameof(productDetail), "El detalle del producto no puede ser null.");

            return productDetail.SubTotal * productDetail!.Product!.Tax.Percentage;
        }

        public Invoice CalculateInvoiceDetails(Invoice invoice)
        {
            if (invoice is null)
                throw new ArgumentNullException(nameof(invoice), "La factura no puede ser null.");
            
            // Se calculan cada uno de los productos 
            foreach (var item in invoice.Products)
            {
                item.SubTotal = CalculateSubTotal(item);
                item.Taxes = CalculateTaxes(item);
            }
            return invoice;
        }
    }
}
