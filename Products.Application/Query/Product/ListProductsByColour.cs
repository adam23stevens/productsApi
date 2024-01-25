using System;
using MediatR;
using Products.Model.Dto;

namespace Products.Application.Query.Product
{
	public class ListProductsByColour : IRequest<IEnumerable<ProductListDto>>
	{
		public string ColourName { get; }
		public ListProductsByColour(string colourName)
		{
			ColourName = colourName.ToUpper();
		}
	}
}

