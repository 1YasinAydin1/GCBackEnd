using GCBackEnd.Application.Repositories;
using GCBackEnd.Domain.Entities.Common;
using GCBackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{
		private readonly GCBackEndDbContext dbContext;

		public WriteRepository(GCBackEndDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public DbSet<T> Table => dbContext.Set<T>();
		public async Task<bool> AddAsync(T entity)
		{
			EntityEntry<T> entityEntry = await Table.AddAsync(entity);
			return entityEntry.State == EntityState.Added;
		}

		public async Task<bool> AddRangeAsync(List<T> entity)
		{
			await Table.AddRangeAsync(entity);
			return true;
		}

		public bool Remove(T entity)
		{
			EntityEntry<T> entityEntry = Table.Remove(entity);
			return entityEntry.State == EntityState.Added;
		}

		public bool RemoveById(string id)
		{
			T model = Table.FirstOrDefault(i => i.Id.Equals(Guid.Parse(id)));
			return Remove(model);
		}

		public async Task<int> SaveAsync()
			=> await dbContext.SaveChangesAsync();

		public bool Update(T entity)
		{
			EntityEntry<T> entityEntry = Table.Update(entity);
			return entityEntry.State == EntityState.Modified;
		}
	}
}
