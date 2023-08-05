﻿using GCBackEnd.Application.Repositories;
using GCBackEnd.Domain;
using GCBackEnd.Domain.Entities;
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
	public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
	{
		public OrderReadRepository(GCBackEndDbContext dbContext) : base(dbContext)
		{
		}
	}
}
