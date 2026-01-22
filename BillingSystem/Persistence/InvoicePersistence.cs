using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BillingSystem.Models;

namespace BillingSystem.Persistence
{
    internal class InvoicePersistence : IPersistence<Invoice>
    {
        private readonly string _path = "invoice.json";
        public void Save(List<Invoice> invoices)
        {
            var jsonOption = new JsonSerializerOptions() { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize<List<Invoice>>(invoices, jsonOption);
            File.WriteAllText(_path, jsonString);
        }
        public List<Invoice> Load()
        {
            if (!File.Exists(_path))
                return new List<Invoice>();

            var jsonInvoices = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Invoice>>(jsonInvoices)
                ?? new List<Invoice>();
        }
    }
}
