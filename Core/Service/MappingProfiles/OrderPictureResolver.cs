using AutoMapper;
using Domain.Models.OrderModules;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    internal class OrderPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderPictureResolver(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.ProductItemOrdered.PictureUrl}";
                return Url;
            }
        }
    }
}
