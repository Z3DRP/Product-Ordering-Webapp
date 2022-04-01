using System;
using System.ComponentModel.DataAnnotations;

namespace Module6_7_Palmer.Models
{
    public class OrderedProduct : Product
    {
        [Required(ErrorMessage = "Enter a quantity")]
        public int? Quantity { get; set; }

        //[Required(ErrorMessage = "Enter a price")]
        //public double? Price 
        //{
        //    get => Price.Value;
        //    set => CalculateProductPrice(); 
        //}

        public double CalculateProductPrice()
        {
            double price = 0;

            if (Quantity.HasValue && UnitPrice > 0)
                price = Quantity.Value * UnitPrice;

            return price;
        }
    }
}
