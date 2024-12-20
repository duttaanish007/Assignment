using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Company_Name { get; set; }
    }
}
