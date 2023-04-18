using BankManagement_ManagementAPI.Models.DTO;

namespace BankManagement_ManagementAPI.Data
{
    public static class BankStore
    {
        public static List<BankDTO> bankList = new List<BankDTO>
        {
                new BankDTO{AccNo = 534636, AccName="Vitesh", AadharCard = 5297865, PanCard = "jrtjr744", Address = "delhi"},
                new BankDTO{AccNo = 6734636, AccName="Sethi", AadharCard = 7609678, PanCard = "hgfdj4574", Address = "dehradun"}
        };
    }
}
