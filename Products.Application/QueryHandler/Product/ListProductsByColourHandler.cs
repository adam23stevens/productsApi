using System;
using MediatR;
using Products.Application.Query.Product;
using Products.DAL.Contract;
using Products.Model.Dto;

namespace Products.Application.QueryHandler.Product
{
	public class ListProductsByColourHandler : IRequestHandler<ListProductsByColour, IEnumerable<ProductListDto>>
	{
        private IRepository<DAL.Entity.Product, ProductDetailDto, ProductListDto> _repository;
		public ListProductsByColourHandler(
            IRepository<DAL.Entity.Product, ProductDetailDto, ProductListDto> repository)
		{
            _repository = repository;
		}

        public async Task<IEnumerable<ProductListDto>> Handle(ListProductsByColour request, CancellationToken cancellationToken)
        {
            try
            {
                var ret = await _repository.ListWhereAsync(
                    x => x.Colour != null && x.Colour.Name.ToUpper() == request.ColourName);

                return ret;
            }
            catch (Exception ex)
            {
                //TODO - logging
                throw;
            }
        }
    }
}

