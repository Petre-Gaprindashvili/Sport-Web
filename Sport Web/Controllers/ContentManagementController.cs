//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Sport_Web.Abstraction;
//using Sport_Web.DTO;

//namespace Sport_Web.Controllers
//{
//	[ApiController]
//	[Route("api/admin/content")]
//	[Authorize(Roles = "Admin")]
//	public class ContentManagementController : ControllerBase
//	{
//		private readonly IcontentService _contentService;

//		public ContentManagementController(IcontentService contentService)
//		{
//			_contentService = contentService;

//		}

//		//[HttpPost("Home")]
//		//public async Task<IActionResult> AddHome(HomeDto homeDto)
//		//{
//		//	var team = await _contentService.AddHomeAsync(homeDto);
//		//	return Ok(team);
//		//}


//		//[HttpPut("UpdateHome/{id}")]
//		//public async Task<IActionResult> UpdateHome(int id, HomeDto homeDto)
//		//{
//		//	var home = await _contentService.UpdateHomeAsync(id, homeDto);
//		//	return Ok(home);
//		//}


//		//[HttpDelete("DeleteHome/{id}")]
//		//public async Task<IActionResult> DeleteHome(int id)
//		//{
//		//	var home = await _contentService.DeleteHomeAsync(id);
//		//	return Ok(home);
//		//}

//		[HttpPost("Team")]
//		public async Task<IActionResult> AddTeam(TeamDto teamDto)
//		{
//			var team = await _contentService.AddTeamAsync(teamDto);
//			return Ok(team);
//		}

//		[HttpPut("UpdateTeam/{id}")]
//		public async Task<IActionResult> UpdateTeam(int id, TeamDto teamDto)
//		{
//			var teams = await _contentService.UpdateTeamAsync(id, teamDto);
//			return Ok(teams);
//		}

//		[HttpDelete("DeleteTeam/{id}")]
//		public async Task<IActionResult> DeleteTeam(int id)
//		{
//			var team = await _contentService.DeleteTeamAsync(id);
//			return Ok(team);
//		}

//		[HttpPost("Matches")]
//		public async Task<IActionResult> AddMatches(MatchesDto matchesDto)
//		{
//			var matches = await _contentService.AddMatchesAsync(matchesDto);
//			return Ok(matches);
//		}

//		[HttpPut("UpdateMatches/{id}")]
//		public async Task<IActionResult> UpdateMatches(int id, MatchesDto matchesDto)
//		{
//			var matches = await _contentService.UpdateMatchesAsync(id, matchesDto);
//			return Ok(matches);
//		}

//		[HttpDelete("DeleteMatches/{id}")]
//		public async Task<IActionResult> DeleteMatches(int id)
//		{
//			var home = await _contentService.DeleteMatchesAsync(id);
//			return Ok(home);
//		}

//		[HttpPost("News")]
//		public async Task<IActionResult> AddNews(NewsDto newsDto)
//		{
//			var news = await _contentService.AddNewsAsync(newsDto);
//			return Ok(news);
//		}

//		[HttpPut("UpdateNews/{id}")]
//		public async Task<IActionResult> UpdateNews(int id, NewsDto newsDto )
//		{
//			var updatedNews = await _contentService.UpdateNewsAsync(id, newsDto);
//			return Ok(updatedNews);
//		}

//		[HttpDelete("DeleteNews/{id}")]
//		public async Task<IActionResult> DeleteNews(int id)
//		{
//			var news = await _contentService.DeleteNewsAsync(id);
//			return Ok(news);
//		}

//		[HttpPost("AddPlayer")]
//		public async Task<IActionResult> AddPlayer( PlayerDto playerDto)
//		{
//			var players = await _contentService.AddPlayerAsync(playerDto);
//			return Ok(players);	
//		}
//		[HttpGet("GetPlayer{id}")]
//		public async Task<ActionResult<PlayersResponseDto>> GetPlayerById(int id)
//		{
//			var player = await _contentService.GetPlayerByIdAsync(id);
//			return Ok(player);
//		}

//		[HttpPut("updatePlayer/{id}")]
//		public async Task<ActionResult<PlayersResponseDto>> UpdatePlayer(int id, PlayerDto playerDto)
//		{
//			var updatedPlayer = await _contentService.UpdatePlayerAsync(id, playerDto);
//			return Ok(updatedPlayer);
//		}

//		[HttpDelete("deletePlayer/{id}")]
//		public async Task<ActionResult> DeletePlayer(int id)
//		{
//			var result = await _contentService.DeletePlayerAsync(id);
//			return Ok(result);
//		}

//		[HttpPost("AddProduct")]
//		public async Task<IActionResult> AddProduct(ProductDtocs productDtocs)
//		{
//			var players = await _contentService.AddProductAsync(productDtocs);
//			return Ok(players);
//		}

//		[HttpGet("GetProduct{id}")]
//		public async Task<ActionResult<PlayersResponseDto>> GetProductById(int id)
//		{
//			var product = await _contentService.GetProductByIdAsync(id);
//			return Ok(product);
//		}

//		[HttpPut("updateProduct/{id}")]
//		public async Task<ActionResult<ProductResponseDtocs>> UpdateProduct(int id, ProductDtocs productDto)
//		{
//			var updatedProduct = await _contentService.UpdateProductAsync(id, productDto);
//			return Ok(updatedProduct);
//		}

//		[HttpDelete("deleteProduct/{id}")]
//		public async Task<ActionResult> DeleteProduct(int id)
//		{
//			var result = await _contentService.DeleteProductAsync(id);
//			return Ok(result);
//		}

//	}

//}





