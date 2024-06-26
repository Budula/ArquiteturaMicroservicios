﻿using AutoMapper;
using GeekShooping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;

namespace GeekShooping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>().ReverseMap();
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                config.CreateMap<CartVO, Cart>().ReverseMap();
                
            });
            return mappingConfig;
        }
    }
}
