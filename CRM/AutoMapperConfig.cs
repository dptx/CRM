using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CRM.Models;
using CRM.DataAccess.Models;

namespace CRM
{
    public sealed class AutoMapperConfig
    {
        private static readonly AutoMapperConfig instance = new AutoMapperConfig();

        static AutoMapperConfig() { }

        private AutoMapperConfig() { }

        public IMapper Mapper
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfile());
                });

                return config.CreateMapper();
            }
        }

        public static AutoMapperConfig Instance { get { return instance; } }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.AccountModel, DataAccess.Models.Account>();

            CreateMap<Models.CustomerModel, DataAccess.Models.Customer>()
                .ReverseMap();

            CreateMap<Models.NoteModel, DataAccess.Models.Note>()
                .ReverseMap();

        }

        //protected override void Configure()
        //{
            //CreateMap<RegisterViewModel, Account>();
            //CreateMap<WS.Account, DTO.Account>();
            //.ForSourceMember(source => source.OriginationCountry, opt => opt.Ignore())
            //.ForSourceMember(source => source.ProductType, opt => opt.Ignore())
            //.ForSourceMember(source => source.Vehicle, opt => opt.Ignore())
            //.ForSourceMember(source => source.Dealer, opt => opt.Ignore())
            //.ForSourceMember(source => source.ContactInfo, opt => opt.Ignore())
            //.ReverseMap();

            //CreateMap<WS.Subscription, DTO.Subscription>();
            ////.ReverseMap();

            //CreateMap<WS.CustomerRole, DTO.CustomerRole>();
            ////.ForSourceMember(source => source.ProductType, opt => opt.Ignore())
            ////.ReverseMap();

            //CreateMap<WS.Profile, DTO.Profile>()
            //    .ReverseMap();

            //CreateMap<WS.SecurityQuestion, DTO.SecurityQuestion>()
            //    .ReverseMap();

            //CreateMap<WS.LoginReturn, DTO.LoginReturn>();
            ////.ForSourceMember(source => source.StatusCode, opt => opt.Ignore());

            //CreateMap<WS.LoginStatus, DTO.ProfileStatus>();

            //CreateMap<WS.ConfirmationKeyStatus, DTO.ConfirmKeyStatus>();
        //}
    }
}