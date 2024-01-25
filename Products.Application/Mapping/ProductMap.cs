using System;
using AutoMapper;
using Products.DAL.Entity;
using Products.Model.Dto;

namespace Products.Application.Mapping
{
	public class ProductMap : Profile
	{
		public ProductMap()
		{
			CreateMap<Product, ProductListDto>();
			CreateMap<Product, ProductDetailDto>();
		}
	}
}

