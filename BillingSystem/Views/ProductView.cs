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
    internal class ProductView
    {
        private ProductService _productService;
        private TaxService _taxService;
        public ProductView(ProductService productService,TaxService taxService)
        {
            _productService = productService;
            _taxService = taxService;
        }
        public void ProductMenu()
        {
            while (true) 
            {
                Console.Write("-- Gestor de productos --" +
                        "\n1. Nuevo producto." +
                        "\n2. Eliminar producto." +
                        "\n3. Mostrar Productos" +
                        "\n0. Volver atras." +
                        "\n>> ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateProduct();
                        break;
                    case "2":
                        DeleteProduct();
                        break;
                    case "3":
                        ShowProducts();
                        break;
                    case "0":
                        return;
                    default:
                        ConsoleUtils.ClearAndWrite("-- Opcion invalida --");
                        ConsoleUtils.PressAndContinue();
                        break;
                }
            }
        }
        public void CreateProduct()
        {
            Console.Clear();
            Console.WriteLine("-- Nuevo Producto --");

            Console.Write("Nombre de producto: ");
            var productName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(productName))
            {
                ConsoleUtils.ClearAndWrite("-- El nombre del producto no puede estar vacio --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            Console.Write("Descripcion: ");
            var productDescription = Console.ReadLine();

            Console.Write("Precio: ");
            if(!double.TryParse(Console.ReadLine(), out var productPrice) || productPrice < 1)
            {
                ConsoleUtils.ClearAndWrite("-- Error. Se esperaba un numero positivo --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            var taxes = _taxService.GetTaxes();
            Console.WriteLine("Selecciona el tipo de impuesto");
            for (var i = 0; i < taxes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {taxes[i].Name}  ({taxes[i].Percentage * 100}%)");
            }
            Console.Write("Opcion: ");
            Itbis taxSelected;
            try
            {
                var taxOption = (int.Parse(Console.ReadLine()!)-1);
                taxSelected = _taxService.GetTax(taxOption);
            }catch(Exception ex)
            {
                ConsoleUtils.ClearAndWrite(ex.Message);
                ConsoleUtils.PressAndContinue();
                return;
            }

            var product = new Product(productName!, productDescription!, productPrice, taxSelected);
            
            try
            {
                _productService.AddProduct(product);
                ConsoleUtils.ClearAndWrite("-- Producto agregado correctamente --");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ConsoleUtils.PressAndContinue();
        }
        public void DeleteProduct()
        {
            
            ShowProducts();

            Console.Write("Option a eliminar ('0' para volver): ");
            if (!int.TryParse(Console.ReadLine(), out var option))
            {
                ConsoleUtils.ClearAndWrite("-- Error. Se esperaba un numero positivo --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            if (option == 0) return;
            try
            {
                _productService.DeleteProduct(option-1);
                ConsoleUtils.ClearAndWrite("-- Producto eliminado correctamente --");
            } catch (Exception ex)
            {
                ConsoleUtils.ClearAndWrite(ex.Message);
            }

            ConsoleUtils.PressAndContinue();
        }
        public void ShowProducts()
        {
            var products = _productService.GetProducts();
            if(products.Count < 1)
            {
                ConsoleUtils.ClearAndWrite("-- No hay productos actualmente --");
                ConsoleUtils.PressAndContinue();
                return;
            }

            ConsoleUtils.ClearAndWrite("\n-- Lista de productos --\n   Nombre | Precio");
            for (var i = 0; i < products.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {products[i].Name}: {products[i].Price} RD$");
            }
        }
    }
}
