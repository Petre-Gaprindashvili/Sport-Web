using Sport_Web.Abstraction;
using Microsoft.EntityFrameworkCore;
namespace Sport_Web.Implementation
{
	public class ImageUploadService: IImageUploadService
	{
		private readonly IWebHostEnvironment _env;

		public ImageUploadService(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task<string> UploadImageAsync(IFormFile image)
		{
			if (image == null) return null;

			// Generate a unique name for the image file
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
			var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

			// Create directory if it doesn't exist
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));

			// Save the image to disk
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}

			return $"/images/{fileName}";
			

		}

	}
}
