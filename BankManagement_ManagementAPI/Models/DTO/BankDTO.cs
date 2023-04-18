using System.ComponentModel.DataAnnotations;

namespace BankManagement_ManagementAPI.Models.DTO
{
    public class BankDTO
    {
        public int AccNo { get; set; }
        [Required]
        [MaxLength(255)]
        public string AccName { get; set; }

        public int AadharCard { get; set; }
        public string PanCard { get; set; }
        public string Address { get; set; }
    }
}
