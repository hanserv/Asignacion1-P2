using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Models;

namespace BillingSystem.Calculators
{
    internal class InvoiceCalculator
    {
        public double CalculateSubTotal(Invoice invoice)
        {
            if(invoice is null)
                throw new ArgumentNullException(nameof(invoice),"La factura no puede ser null.");

            double total = 0;
            foreach (var productDetail in invoice.Products)
            {
                total += productDetail.SubTotal;
            }
            return total;
        }
        public double CalculateTaxes(Invoice invoice)
        {
            if (invoice is null)
                throw new ArgumentNullException(nameof(invoice), "La factura no puede ser null.");

            double total = 0;
            foreach (var productDetail in invoice.Products)
            {
                total += productDetail.Taxes;
            }
            return total;
        }
        public double CalculateTotal(Invoice invoice)
        {
            if (invoice is null)
                throw new ArgumentNullException(nameof(invoice), "La factura no puede ser null.");

            return invoice.SubTotal + invoice.Taxes;
        }

        public int NextId(List<Invoice> invoices)
        {
            int maxIdInvoice = 0;
            foreach (var invoice in invoices)
            {
                if(invoice.Id > maxIdInvoice)
                    maxIdInvoice = invoice.Id;
            }
            return maxIdInvoice+1;
        }

        public Invoice CalculateInvoice(Invoice invoice)
        {
            invoice.SubTotal = CalculateSubTotal(invoice);
            invoice.Taxes = CalculateTaxes(invoice);
            invoice.Total = CalculateTotal(invoice);

            return invoice;
        }
    }
}
