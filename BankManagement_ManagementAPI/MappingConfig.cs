using AutoMapper;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BankManagement_ManagementAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Bank, BankDTO>();
            CreateMap<BankDTO, Bank>();
            CreateMap<Bank, BankCreateDTO>().ReverseMap();
            CreateMap<Bank, BankUpdateDTO>().ReverseMap();


        }
    }
}
