using System;
using System.ComponentModel.DataAnnotations;

namespace CreditDemo.Models
{
    public class CreditCardItem
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name is longer than 50")]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(100000000000, 9999999999999999999, ErrorMessage = "Credit card number must be between 12 and 19 digits")]
        public long CreditCard { get; set; }

        [RegularExpression("(^[0-9]{3,4}$)", ErrorMessage = "Please enter valid Number for CVC")]
        [Required]
        public int CVC { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please enter valid Date")]
        [Required]
        public DateTime ExpiryDate { get; set; }

    }
}
