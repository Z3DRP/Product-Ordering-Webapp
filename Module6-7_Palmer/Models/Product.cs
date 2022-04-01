using System;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Module6_7_Palmer.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Enter a product name")]
        [MaxLength(100, ErrorMessage = "Product name must be 100 characters or less")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter a product description")]
        [MaxLength(255, ErrorMessage = "Product description must be 255 characters or less")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter a unit price")]
        public double UnitPrice { get; set; }

        [Required(ErrorMessage = "Select a image")]
        public string Image { get; set; }

        public string Slug => Name?.Replace(' ', '-').ToLower()
                          + '-' + ProductID.ToString();

    }
}
