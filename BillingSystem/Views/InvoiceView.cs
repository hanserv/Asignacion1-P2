using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Models;
using BillingSystem.Services;
using BillingSystem.Utils;

namespace BillingSystem.Views
{
    internal class InvoiceView
    {
        private InvoiceService _invoiceService;
        private ProductService _productService;
        public InvoiceView(InvoiceService invoiceService,ProductService productService)
        {
            _invoiceService = invoiceService;
            _productService = productService;
        }

        public void InvoiceMenu()
        {
            _invoiceService.NewInvoice();

            while (true)
            {
                ConsoleUtils.ClearAndWrite("-- Creando factura --" +
                    "\n1. Agregar producto.." +
                    "\n2. Quitar producto." +
                    "\n3. Ver productos añadidos." +
                    "\n4. Finalizar factura." +
                    "\n0. Cancelar." +
                    "\n>> ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        RemoveProductFromInvoice();
                        break;
                    case "3":
                        ShowInvoiceProducts();
                        ConsoleUtils.PressAndContinue();
                        break;
                    case "4":
                        GenerateInvoice(); 
                        return;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("-- Option invalida --");
                        ConsoleUtils.PressAndContinue();
                        break;
                }
            }
        }
        public void AddProduct()
        {
            Console.Clear();
            var products = _productService.GetProducts();

            ConsoleUtils.ClearAndWrite("-- Agregando producto --");
            Console.WriteLine("Lista de productos disponibles:\n    Nombre | Precio");
            for(var i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i+1}. {products[i].Name}: {products[i].Price}");
            }

            Console.Write("Option: ");
            if(!int.TryParse(Console.ReadLine(), out var option))
            {
                ConsoleUtils.ClearAndWrite("-- Error. Se esperaba un numero positivo --");
                ConsoleUtils.PressAndContinue();
                return;
            }
            if((option-1) < 0 || (option-1) >= products.Count)
            {
                ConsoleUtils.ClearAndWrite("-- Id no valido --");
                ConsoleUtils.PressAndContinue();
                return;
            }
            var product = products[option-1];
            
            Console.Write("Cantidad: ");
            if(!int.TryParse(Console.ReadLine(),out var quantity) || quantity < 1)
            {
                ConsoleUtils.ClearAndWrite("-- Error. Se esperaba un numero positivo --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            var productDetail = new ProductDetail(product,quantity);
            try
            {
                _invoiceService.AddProductToInvoice(productDetail);
                ConsoleUtils.ClearAndWrite("-- Producto agregado correctamente --");
            }catch (Exception ex)
            {
                ConsoleUtils.ClearAndWrite(ex.Message);
            }
            ConsoleUtils.PressAndContinue();
        }
        public void RemoveProductFromInvoice()
        {
            var invoiceProducts = _invoiceService.GetInvoiceProducts();
            if (invoiceProducts.Count < 1)
            {
                ConsoleUtils.ClearAndWrite("-- No hay productos en la factura --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            ShowInvoiceProducts();
            Console.Write("Select product to remove: ");
            if(!int.TryParse(Console.ReadLine(), out var option) || option < 0)
            {
                ConsoleUtils.ClearAndWrite("-- Error. Se esperaba un numero positivo --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            try
            {
                _invoiceService.RemoveProductFromInvoice(id: option-1);
                ConsoleUtils.ClearAndWrite("-- Producto eliminado de la factura --");
            }catch(Exception ex)
            {
                ConsoleUtils.ClearAndWrite(ex.Message);
            }
            ConsoleUtils.PressAndContinue();
        }
        public void ShowInvoices()
        {
            var invoices = _invoiceService.GetInvoices();
            if (invoices.Count < 1)
            {
                ConsoleUtils.ClearAndWrite("-- No hay facturas para mostrar --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            ConsoleUtils.ClearAndWrite("---- INVOICES ----");
            foreach (var invoice in invoices)
            {
                Console.WriteLine("------------------");
                Console.WriteLine($"Invoice Id: #{invoice.Id}");
                Console.WriteLine($"Date: {invoice.Date:dd-MM-yyyy hh:mm:ss tt}");
                Console.WriteLine("Products: (Name, Price, Quantity, Tax percentage)");
                foreach (var product in invoice.Products)
                {
                    Console.WriteLine($"   {product.Product.Name}, {product.Product.Price}RD$, {product.Quantity}, {product.Product.Tax.Percentage * 100}%");
                }
                Console.WriteLine($"SubTotal: {invoice.SubTotal:N2} RD$");
                Console.WriteLine($"Taxes: {invoice.Taxes:N2} RD$");
                Console.WriteLine($"Total: {invoice.Total:N2} RD$");
                Console.WriteLine("------------------\n");
            }
            ConsoleUtils.PressAndContinue();
        }
        public void ShowInvoiceProducts()
        {
            var invoiceProducts = _invoiceService.GetInvoiceProducts();
            if (invoiceProducts.Count < 1)
            {
                ConsoleUtils.ClearAndWrite("-- No hay productos en la factura --");
                return;
            }

            ConsoleUtils.ClearAndWrite("\n-- Products --\nName | Quantity");
            for (var i = 0; i < invoiceProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {invoiceProducts[i].Product.Name}: {invoiceProducts[i].Quantity}");
            }
        }
        public void GenerateInvoice()
        {
            Console.Clear();
            try
            {
                _invoiceService.Save();
                ConsoleUtils.ClearAndWrite("-- Se guardo la factura correctamente --");
            }
            catch (Exception ex)
            {
                ConsoleUtils.ClearAndWrite(ex.Message);
            }
            ConsoleUtils.PressAndContinue();
        }
    }
}
