using System;
using System.Linq.Expressions;

namespace Products.DAL.Contract
{
	public interface IRepository<TEntity, TDetailDto, TListDto>
	{
		public Task<IEnumerable<TListDto>> ListAsync(
			params Expression<Func<TEntity, object>>[] includes);

		public Task<IEnumerable<TListDto>> ListWhereAsync(
			Expression<Func<TEntity, bool>> wc,
			params Expression<Func<TEntity, object>>[] includes);
	}
}

