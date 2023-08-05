using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBackEnd.Application.ViewModels.Products
{
	public class Product_Create_ViewModel
	{
		public string Name { get; set; }
		public int OnHand { get; set; }
		public float Price { get; set; }
	}
}
