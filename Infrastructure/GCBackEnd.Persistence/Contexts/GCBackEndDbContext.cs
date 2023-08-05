using GCBackEnd.Domain;
using GCBackEnd.Domain.Entities;
using GCBackEnd.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Persistence.Contexts
{
	public class GCBackEndDbContext : DbContext
	{
		public GCBackEndDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Customer> Customers { get; set; }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var item in ChangeTracker.Entries<BaseEntity>())
			{
				switch (item.State)
				{
					case EntityState.Modified:
						item.Entity.UpdateDate = DateTime.UtcNow;
						break;
					case EntityState.Added:
						item.Entity.CreateDate = DateTime.UtcNow;
						break;
					default:
						break;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
