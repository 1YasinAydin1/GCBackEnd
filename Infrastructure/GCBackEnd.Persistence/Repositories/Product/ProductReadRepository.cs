using GCBackEnd.Application.Repositories;
using GCBackEnd.Domain.Entities;
using GCBackEnd.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Persistence.Repositories
{
	public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
	{
		public ProductReadRepository(GCBackEndDbContext dbContext) : base(dbContext)
		{
		}
	}
}
