using System;
using MediatR;
using Products.Model.Dto;

namespace Products.Application.Query.Product
{
	public class ListAllProducts : IRequest<IEnumerable<ProductListDto>>
	{
		
	}
}

