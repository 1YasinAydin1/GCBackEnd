using GCBackEnd.Application.Repositories;
using GCBackEnd.Application.RequestParameters;
using GCBackEnd.Application.ViewModels.Products;
using GCBackEnd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCBackEnd.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository,
								  IWebHostEnvironment webHostEnvironment)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] Pagination pagination)
		{
			var products = _productReadRepository.GetAll(false)
				.Select(i => new
				{
					i.Id,
					i.Name,
					i.OnHand,
					i.Price,
					i.CreateDate,
					i.UpdateDate
				});
			int countProducts = products.Count();
			var pagedProducts = products.OrderBy(i => i.CreateDate).Skip(pagination.Size * pagination.Page).Take(pagination.Size);
			return Ok(new
			{
				countProducts,
				pagedProducts
			});
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetById(string Id)
		{
			return Ok(await _productReadRepository.GetByIdAsync(Id, false));
		}

		[HttpPost]
		public async Task<IActionResult> Post(Product_Create_ViewModel model)
		{
			await _productWriteRepository.AddAsync(new()
			{
				Name = model.Name,
				OnHand = model.OnHand,
				Price = model.Price,
			});
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Upload()
		{
			string path = Path.Combine(_webHostEnvironment.WebRootPath, "resource");

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			foreach (IFormFile file in Request.Form.Files)
			{
				string fullPath = Path.Combine(path, $"{DateTime.Now.ToString("yyyyMMddhhmmss")}{Path.GetExtension(file.FileName)}");
				using FileStream fileStream = new FileStream(fullPath,FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);

				await file.CopyToAsync(fileStream);
				await fileStream.FlushAsync();
			}
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Put(Product_Update_ViewModel model)
		{
			Product p = await _productReadRepository.GetByIdAsync(model.Id);
			p.OnHand = model.OnHand;
			p.Price = model.Price;
			p.Name = model.Name;
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(string Id)
		{
			_productWriteRepository.RemoveById(Id);
			await _productWriteRepository.SaveAsync();
			return Ok();
		}
	}
}
