using System;
namespace Products.Model.Auth
{
	public class SignInResponseDTO
	{
		public bool IsAuthSuccessful { get; set; }
		public string Token { get; set; }
		public string UserId { get; set; }
		public string Email { get; set; }
	}
}

