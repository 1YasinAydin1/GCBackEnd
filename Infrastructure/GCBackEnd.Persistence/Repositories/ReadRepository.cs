using GCBackEnd.Application.Repositories;
using GCBackEnd.Domain.Entities.Common;
using GCBackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
	{
		private readonly GCBackEndDbContext dbContext;

		public ReadRepository(GCBackEndDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public DbSet<T> Table => dbContext.Set<T>();

		public IQueryable<T> GetAll(bool isTracking = true)
		{
			if (!isTracking)
				return Table.AsQueryable().AsNoTracking();
			return Table;
		}
		public async Task<T> GetByIdAsync(string id, bool isTracking = true)
		{
			if (!isTracking)
				return Table.AsQueryable().AsNoTracking().First(i=>i.Id.Equals(Guid.Parse(id)));
			return Table.First(i => i.Id.Equals(Guid.Parse(id)));
		}
		//=> await Table.FindAsync(Guid.Parse(id));

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool isTracking = true)
		{
			if (!isTracking)
				return await Table.AsQueryable().AsNoTracking().FirstAsync(method);
			return await Table.FirstAsync(method);
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool isTracking = true)
		{
			if (!isTracking)
				return Table.AsQueryable().AsNoTracking().Where(method);
			return Table.Where(method);
		}
	}
}
