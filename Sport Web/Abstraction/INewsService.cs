using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface INewsService
	{
		Task<List<NewsResponseDto>> GetNewsByCategoryIdAsync(int CategoryId);

		Task<NewsResponseDto> AddNewsAsync(NewsDto newsDto);
		Task<NewsResponseDto> UpdateNewsAsync(int id, NewsDto newsDto);
		Task<bool> DeleteNewsAsync(int id);

	}
}
