using System;
using blog.DAL;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace blog.Controllers
{
    public class Login
    {
        public int UserId { get; set; }
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    } 
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public LoginController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("/register")]
        public IActionResult Register(User user)
        {
            UserRepository action = new UserRepository();
            var resposne = action.Create(user);
            return Ok(resposne);
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("/login")]
        public IActionResult Login(Login credentials)
        {
            Login login = new Login();
            login.EmailOrUsername = credentials.EmailOrUsername;
            login.Password = credentials.Password;
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);
            if(user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr, user });
            }
            return CreatedAtAction(nameof(Login), response);
        }

        private User AuthenticateUser(Login login)
        {
            User user = null;
            UserRepository userRepository = new UserRepository();
            user = userRepository.getUserByEmail(login.EmailOrUsername);
            if (user == null)
            {
                user = userRepository.getUserByUsername(login.EmailOrUsername);
            }
            if (user != null && user.Password == login.Password)
            {
                return user;
            } else {
                return null;
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("/restore-login")]
        public IActionResult RestoreLogin(Login login)
        {
            UserRepository userRepository = new UserRepository();
            IActionResult response = Unauthorized();
            bool isTokenValid = ValidateCurrentToken(login.Token);
            User user = userRepository.getUserById(login.UserId);
            if (isTokenValid) {
                response = Ok(new { token = login.Token, user});
            }
            return response;
        }
        private bool ValidateCurrentToken(string token)
        {
        	var tokenHandler = new JwtSecurityTokenHandler();
        	try
        	{
        		tokenHandler.ValidateToken(token, new TokenValidationParameters
        		{
        			ValidateIssuerSigningKey = true,
        			ValidateIssuer = true,
        			ValidateAudience = true,
        			ValidIssuer = _configuration["Jwt:Issuer"],
        			ValidAudience = _configuration["Jwt:Issuer"],
        			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]))
        		}, out SecurityToken validatedToken);
        	}
        	catch
        	{
        		return false;
        	}
        	return true;
        }
        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to: " + userName;
        }
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }
    }
}
