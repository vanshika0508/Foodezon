using System.Collections.Generic;
using System.Linq;

namespace Foodezon.Api.Models.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal Subtotal => Items.Sum(i => i.LineTotal);

        public decimal TaxRate { get; set; } = 0.13m;      // 13% tax
        public decimal DeliveryFee { get; set; } = 4.99m;  // flat fee

        public decimal TaxAmount => decimal.Round(Subtotal * TaxRate, 2);
        public decimal Total => Subtotal + TaxAmount + DeliveryFee;
    }
}
