using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.AcceptanceTests.Models
{
    public class CustomerValidator
    {
        public static ICollection<ValidationResult> ValidateCustomer(Customer customer, Func<string, Task<bool>> isEmailUnique)
        {
            var results = new List<ValidationResult>();

            ValidatePhoneNumber(customer.PhoneNumber, results);
            ValidateEmail(customer.Email, results, isEmailUnique);
            ValidateBankAccountNumber(customer.BankAccountNumber, results);

            return results;
        }

        private static void ValidatePhoneNumber(string phoneNumber, ICollection<ValidationResult> results)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, null);
                if (!phoneNumberUtil.IsValidNumber(parsedPhoneNumber))
                {
                    results.Add(new ValidationResult("Invalid phone number.", new[] { nameof(Customer.PhoneNumber) }));
                }
            }
            catch (NumberParseException)
            {
                results.Add(new ValidationResult("Invalid phone number.", new[] { nameof(Customer.PhoneNumber) }));
            }
        }

        private static async Task ValidateEmail(string email, ICollection<ValidationResult> results, Func<string, Task<bool>> isEmailUnique)
        {
            if (!new EmailAddressAttribute().IsValid(email))
            {
                results.Add(new ValidationResult("Invalid email address.", new[] { nameof(Customer.Email) }));
                return;
            }

            if (!await isEmailUnique(email))
            {
                results.Add(new ValidationResult("Email address must be unique.", new[] { nameof(Customer.Email) }));
            }
        }

        private static void ValidateBankAccountNumber(string bankAccountNumber, ICollection<ValidationResult> results)
        {
            if (string.IsNullOrWhiteSpace(bankAccountNumber))
            {
                results.Add(new ValidationResult("Bank account number is required.", new[] { nameof(Customer.BankAccountNumber) }));
                return;
            }

            if (!IsDigitsOnly(bankAccountNumber))
            {
                results.Add(new ValidationResult("Bank account number must contain only digits.", new[] { nameof(Customer.BankAccountNumber) }));
            }
            else if (bankAccountNumber.Length < 8 || bankAccountNumber.Length > 20)
            {
                results.Add(new ValidationResult("Bank account number length must be between 8 and 20 digits.", new[] { nameof(Customer.BankAccountNumber) }));
            }
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
