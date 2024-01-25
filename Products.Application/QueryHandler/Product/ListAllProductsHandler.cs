using System;
using MediatR;
using Products.Application.Query.Product;
using Products.DAL.Contract;
using Products.Model.Dto;

namespace Products.Application.QueryHandler.Product
{
	public class ListAllProductsHandler : IRequestHandler<ListAllProducts, IEnumerable<ProductListDto>>
	{
        private IRepository<DAL.Entity.Product, ProductDetailDto, ProductListDto> _repository;

		public ListAllProductsHandler(
            IRepository<DAL.Entity.Product, ProductDetailDto, ProductListDto> repository)
		{
            _repository = repository;
		}

        public async Task<IEnumerable<ProductListDto>> Handle(ListAllProducts request, CancellationToken cancellationToken)
        {
            try
            {
                var ret = await _repository.ListAsync();

                return ret;
            }
            catch (Exception ex)
            {
                //TODO - logging / error handling 
                throw;
            }
        }
    }
}

