using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Models;

namespace BillingSystem.Services
{
    internal class TaxService
    {
        private List<Itbis> _taxes;

        public TaxService()
        {
            _taxes = new List<Itbis>()
            {
                new Itbis("General ITBIS", 0.18),
                new Itbis("Reduced ITBIS", 0.16),
                new Itbis("Excempt ITBIS", 0)
            };
        }

        public List<Itbis> GetTaxes()
        {
            return _taxes;
        }

        public Itbis GetTax(int id)
        {
            if(id < 0 || id >=  _taxes.Count)
                throw new ArgumentOutOfRangeException(nameof(id),"Id no valido.");

            return _taxes[id];
        }
    }
}
