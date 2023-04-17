using BankManagement_ManagementAPI.Models.DTO;

namespace BankManagement_ManagementAPI.Data
{
    public static class BankStore
    {
        public static List<BankDTO> bankList = new List<BankDTO>
        {
                new BankDTO{AccNo = 534636, AccName="Vitesh"},
                new BankDTO{AccNo = 6734636, AccName="Sethi"}
        };
    }
}
