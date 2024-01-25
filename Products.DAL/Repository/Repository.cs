using System;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Products.DAL.Contract;
using Products.DAL.Entity;

namespace Products.DAL.Repository
{
	public class Repository<TEntity, TDetailDto, TListDto> :
		IRepository<TEntity, TDetailDto, TListDto>
		where TEntity: BaseGuidEntity 
	{
		private readonly IMapper _mapper;
		private readonly ProductsDbContext _dbContext;

		public Repository(IMapper mapper, ProductsDbContext dbContext)
		{
			_mapper = mapper;
			_dbContext = dbContext;
		}

        public async Task<IEnumerable<TListDto>> ListAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            try
			{
				var baseSet = _dbContext.Set<TEntity>();

				foreach(var inc in includes)
				{
					baseSet.Include(inc);
				}

				return await baseSet.ProjectTo<TListDto>(_mapper.ConfigurationProvider)
					.ToListAsync();
			}

			catch(Exception ex)
			{
				//TODO logging exception
				throw new Exception(ex.Message);
			}
        }

        public async Task<IEnumerable<TListDto>> ListWhereAsync(Expression<Func<TEntity, bool>> wc, params Expression<Func<TEntity, object>>[] includes)
        {
            try
			{
				var baseSet = _dbContext.Set<TEntity>();

                foreach (var inc in includes)
                {
                    baseSet.Include(inc);
                }

				return await baseSet
							.Where(wc)
							.ProjectTo<TListDto>(_mapper.ConfigurationProvider)
							.ToListAsync();
			}

			catch (Exception ex)
			{
				//TODO - logging exception
				throw new Exception(ex.Message);
			}
        }
    }
}

