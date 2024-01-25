using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products.Api.Service;
using Products.Model.Auth;

namespace Products.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class AccountController : BaseApiController
	{
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly APISettings _apiSettings;

		public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<APISettings> apiSettings,
            IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor)
		{
            _signInManager = signInManager;
            _userManager = userManager;
            _apiSettings = apiSettings.Value;
		}

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.Username, signInRequestDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.Username);
                if (user == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var signingCredentials = GetSigningCredentials();
                    var claims = await GetClaimsAsync(user);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: _apiSettings.ValidIssuer,
                        audience: _apiSettings.ValidAudience,
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: signingCredentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(new SignInResponseDTO()
                    {
                        IsAuthSuccessful = true,
                        Token = token,
                        UserId = user.Id,
                        Email = user.Email
                    });
                }
            }
            else
            {
                
                return StatusCode(500);
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}

