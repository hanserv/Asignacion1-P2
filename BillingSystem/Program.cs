using BillingSystem.Views;

namespace BillingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new MenuView();
            app.Deploy();
        }
    }
}
