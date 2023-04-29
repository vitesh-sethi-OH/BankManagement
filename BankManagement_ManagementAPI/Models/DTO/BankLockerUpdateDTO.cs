using System.ComponentModel.DataAnnotations;

namespace BankManagement_ManagementAPI.Models.DTO
{
    public class BankLockerUpdateDTO
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public int BankId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
