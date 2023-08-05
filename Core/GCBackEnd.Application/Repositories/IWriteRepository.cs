using GCBackEnd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
	{
		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(List<T> entity);
		bool Remove(T entity);
		bool RemoveById(string id);
		bool Update(T entity);
		Task<int> SaveAsync();
	}
}
