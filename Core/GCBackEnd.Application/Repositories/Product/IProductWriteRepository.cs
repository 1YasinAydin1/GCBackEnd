using GCBackEnd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Application.Repositories
{
	public interface IProductWriteRepository : IWriteRepository<Product>
	{
	}
}
