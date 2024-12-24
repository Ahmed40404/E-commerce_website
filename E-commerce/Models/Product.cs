using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(75)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public string Iamgeurl { get; set; }

    }
}
