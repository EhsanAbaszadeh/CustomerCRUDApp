using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.AcceptanceTests.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{8,20}$", ErrorMessage = "Invalid bank account number.")]
        public string BankAccountNumber { get; set; }
    }
}
