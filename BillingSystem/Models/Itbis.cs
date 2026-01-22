using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Models
{
    internal class Itbis 
    {
        public string? Name { get; set; }
        public double Percentage { get; set; }

        public Itbis(string name,double percentage)
        {
            Name = name;
            Percentage = percentage;
        }
    }
}
