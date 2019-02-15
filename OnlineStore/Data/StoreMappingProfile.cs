using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using AutoMapper;
using OnlineStore.Data.Entities;
using OnlineStore.ViewModels;

namespace OnlineStore.Data 
{
    public class StoreMappingProfile : Profile
    { 
        public StoreMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
