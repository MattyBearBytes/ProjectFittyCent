﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FittyCent.Domain;
using FittyCent.Web.Models;

namespace FittyCent.Web {
    public class AutoMapperConfig {
        public static void Configure() {
            Mapper.CreateMap<UserAccount, UserAccountModel>()
                .ForMember(dst => dst.HasMobileServiceAvailable,
                    map => map.MapFrom(src => src.TrainerProfile.HasMobileServiceAvailable))
                .ForMember(dst => dst.IsInsured, map => map.MapFrom(src => src.TrainerProfile.IsInsured))
                .ForMember(dst => dst.Summary, map => map.MapFrom(src => src.TrainerProfile.Summary))
                .ForMember(dst => dst.Registrations, map => map.MapFrom(src => SplitData(src.TrainerProfile.Registrations)))
                .ForMember(dst => dst.Specialisations, map => map.MapFrom(src => SplitData(src.TrainerProfile.Specialisations)))
                .ForMember(dst => dst.Qualifications, map => map.MapFrom(src => SplitData(src.TrainerProfile.Qualifications)))
                .ForMember(dst => dst.CompanyName, map => map.MapFrom(src => src.TrainerProfile.CompanyName));
        }

        private static IEnumerable<string> SplitData(string toSplit) {
            return string.IsNullOrWhiteSpace(toSplit)
                ? new string[0]
                : toSplit.Split(',').Select(x => x.Trim());
        }
    }
}