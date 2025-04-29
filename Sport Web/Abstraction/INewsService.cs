using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface INewsService
	{
		Task<List<NewsResponseDto>> GetNewsByParentCategoryAsync();
		Task<List<NewsResponseDto>> GetNewsByCategoryIdAsync(int CategoryId);
		Task<List<NewsResponseDto>> GetNewsByTeamIdAsync(int TeamId);
		Task<List<NewsResponseDto>> GetVideoNewsByCategoryIdAsync(int categoryId);

		// Admin-specific methods:
		Task<NewsResponseDto> AddNewsAsync(NewsDto newsDto);
		Task<NewsResponseDto> UpdateNewsAsync(int id, NewsDto newsDto);
		Task<bool> DeleteNewsAsync(int id);

	}
}
