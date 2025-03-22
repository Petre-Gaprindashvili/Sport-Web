namespace Sport_Web.Abstraction
{
	public interface IImageUploadService
	{
		Task<string> UploadImageAsync(IFormFile image);
	}
}
