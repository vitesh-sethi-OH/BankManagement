using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagement_ManagementAPI.Models
{
    public class BankLocker
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountNumber { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}


    }
}
