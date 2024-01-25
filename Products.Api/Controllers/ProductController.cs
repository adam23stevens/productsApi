using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Query.Product;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace Products.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	public class ProductController : BaseApiController
	{
		public ProductController(IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor)
		{
		}

		[HttpGet("Test")]
		public IActionResult Test()
		{
			return Ok();
		}

		[Authorize]
		[HttpGet("All")]
		public async Task<IActionResult> AllProducts()
		{
			try
			{
				var ret = await Mediator.Send(new ListAllProducts());
                return Ok(ret);
            }
			catch(Exception ex)
			{
				//Log exception
				return StatusCode(500);
			}
		}

		[Authorize]
		[HttpGet("ByColour/{colour}")]
		public async Task<IActionResult> AllProductsByColour(string colour)
		{
			try
			{
				var ret = await Mediator.Send(new ListProductsByColour(colour));
				return Ok(ret);
			}
			catch (Exception ex)
			{
				//log
				return StatusCode(500);
			}
		}
	}
}

