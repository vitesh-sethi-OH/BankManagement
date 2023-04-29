using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagement_ManagementAPI.Models
{
    public class Bank
    {
        internal DateTime UpdatedDate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccNo { get; set; }
        public string AccName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AadharCard { get; set; }
        public string PanCard { get; set; }
        public string Address { get; set; }
        public string AccType { get; set; }
    }
}
