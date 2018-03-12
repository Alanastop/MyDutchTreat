using AutoMapper;
using DutchTreat.Library2.Models.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.MVC.ViewModels.Mappings
{
    public class DutchMappingProfile: Profile
    {
        public DutchMappingProfile() => CreateMap<Order, OrderViewModel>()
            .ForMember(order=> order.OrderId, ex => ex.MapFrom(or => or.Id))
            .ReverseMap();

    }
}
