using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Products.Api.Controllers
{
	public class BaseApiController : ControllerBase
	{
		private IMediator _mediator;
		private IHttpContextAccessor _httpContextAccessor;

		public BaseApiController(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected IMediator Mediator
		{
			get
			{
				if (_mediator == null)
				{
					_mediator = HttpContext.RequestServices.GetService<IMediator>();
				}

				return _mediator;
			}
		}
	}
}

