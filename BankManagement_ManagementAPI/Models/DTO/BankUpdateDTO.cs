using System.ComponentModel.DataAnnotations;

namespace BankManagement_ManagementAPI.Models.DTO
{
    public class BankUpdateDTO
    {
        [Required]
        public int AccNo { get; set; }
        [Required]
        [MaxLength(255)]
        public string AccName { get; set; }
        [Required]
        public int AadharCard { get; set; }
        [Required]
        public string PanCard { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AccType { get; set; }
    }
}
