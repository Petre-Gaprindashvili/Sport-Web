using Microsoft.EntityFrameworkCore;
using Sport_Web.Abstraction;
using Sport_Web.Data;
using Sport_Web.DTO;
using Sport_Web.Models;

namespace Sport_Web.Implementation
{
	public class ProductService : IProductService		
	{

		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public ProductService(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			_context = context;
			_imageUploadService = imageUploadService;
		}

		public async Task<List<ProductResponseDtocs>> GetProductsByTeamIdAsync(int teamId)
		{
			var products = await _context.Products.Where(p => p.TeamId == teamId)
							.Select(p => new ProductResponseDtocs
							{
								Id = p.Id,
								Name = p.Name,
								Price = p.Price,
								Description = p.Description,
								ImageUrl = p.ImageUrl,
								Stock = p.Stock,
								IsAvailable = p.IsAvailable,
								TeamId = p.TeamId,

							}).ToListAsync();

			return products;
		}


		public async Task<ProductResponseDtocs> AddProductAsync(ProductDtocs productDto)
		{
			var teamExists = await _context.Teams.FirstOrDefaultAsync(t => t.Id == productDto.TeamId);
			if (teamExists == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			var photoUrl = await _imageUploadService.UploadImageAsync(productDto.ImageUrl);

			var product = new Product
			{
				Name = productDto.Name,
				Price = productDto.Price,
				Description = productDto.Description,
				ImageUrl = photoUrl,
				Stock = productDto.Stock,
				IsAvailable = productDto.IsAvailable,
				TeamId = productDto.TeamId,

			};
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return new ProductResponseDtocs
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Stock = product.Stock,
				IsAvailable = product.IsAvailable,
				TeamId = product.TeamId,
			};

		}



		public async Task<ProductResponseDtocs> GetProductByIdAsync(int productId)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);


			if (product == null)
			{
				var response = new ResponseDto
				{
					IsSuccess = false,
					Message = "Invalid Id"
				};
			}

			return new ProductResponseDtocs
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Stock = product.Stock,
				IsAvailable = product.IsAvailable,
				TeamId = product.TeamId,
			};
		}

		public async Task<ProductResponseDtocs> UpdateProductAsync(int productId, ProductDtocs productDto)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null) throw new ArgumentException("Product not found.");

			var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == productDto.TeamId);
			if (team == null) throw new ArgumentException("Invalid TeamId. Team does not exist.");

			string updatedPhotoUrl = product.ImageUrl;

			updatedPhotoUrl = await _imageUploadService.UploadImageAsync(productDto.ImageUrl);


			product.Name = productDto.Name;
			product.TeamId = productDto.TeamId;
			product.Price = productDto.Price;
			product.Description = productDto.Description;
			product.Stock = productDto.Stock;
			product.IsAvailable = productDto.IsAvailable;
			product.ImageUrl = updatedPhotoUrl;

			await _context.SaveChangesAsync();

			return new ProductResponseDtocs
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Description = product.Description,
				ImageUrl = product.ImageUrl,
				Stock = product.Stock,
				IsAvailable = product.IsAvailable,
				TeamId = product.TeamId,
			};
		}

		public async Task<bool> DeleteProductAsync(int productId)
		{
			var product = await _context.Products.FindAsync(productId);
			if (product == null) return false;

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
