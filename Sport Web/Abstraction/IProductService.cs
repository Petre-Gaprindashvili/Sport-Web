using Sport_Web.DTO;

namespace Sport_Web.Abstraction
{
	public interface IProductService
	{
		Task<List<ProductResponseDtocs>> GetProductsByTeamIdAsync(int teamId);
		Task<ProductResponseDtocs> GetProductByIdAsync(int productId);


		Task<ProductResponseDtocs> AddProductAsync(ProductDtocs productDto);
		Task<ProductResponseDtocs> UpdateProductAsync(int productId, ProductDtocs productDto);
		Task<bool> DeleteProductAsync(int playerId);


	}
}


