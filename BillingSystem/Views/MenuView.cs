using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Calculators;
using BillingSystem.Services;
using BillingSystem.Utils;

namespace BillingSystem.Views
{
    internal class MenuView
    {
        // Product
        private ProductService _productService;
        private TaxService _taxService;

        // Invoice
        private InvoiceService _invoiceService;

        // Views
        private ProductView _productView;
        private InvoiceView _invoiceView;

        public MenuView()
        {
            _productService = new ProductService();
            _taxService = new TaxService();

            _invoiceService = new InvoiceService();

            _productView = new ProductView(_productService,_taxService);
            _invoiceView = new InvoiceView(_invoiceService,_productService);
        }
        public void Deploy()
        {
            while (true)
            {
                ConsoleUtils.ClearAndWrite("-- Billing System --" +
                    "\n1. Gestionar productos." +
                    "\n2. Crear factura." +
                    "\n3. Mostrar facturas." +
                    "\n0. Salir." +
                    "\n>> ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        _productView.ProductMenu();
                        break;
                    case "2":
                        _invoiceView.InvoiceMenu();
                        break;
                    case "3":
                        _invoiceView.ShowInvoices();
                        break;
                    case "0":
                        ConsoleUtils.ClearAndWrite("-- Programa cerrado --");
                        return;
                    default:
                        Console.WriteLine("-- Opcion invalida --");
                        ConsoleUtils.PressAndContinue();
                        break;
                }
            }
        }
    }
}
