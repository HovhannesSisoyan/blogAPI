using System;
using blog.DAL;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace blog.Controllers
{
    public class LoginCredential
    {
        public int UserId { get; set; }
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public static string GetHashedPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
            }
    } 
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserRepository _userRepository = new UserRepository();

        public LoginController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("/register")]
        public IActionResult Register(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            user.Salt = salt;
            user.Password = LoginCredential.GetHashedPassword(user.Password, salt);

            var resposne = _userRepository.Create(user);
            return Ok(resposne);
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("/login")]
        public IActionResult Login(LoginCredential credentials)
        {
            LoginCredential login = new LoginCredential();
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

        private User AuthenticateUser(LoginCredential login)
        {
            User user = null;
            user = _userRepository.getUserByEmail(login.EmailOrUsername);
            if (user == null)
            {
                user = _userRepository.getUserByUsername(login.EmailOrUsername);
            }
            if (user != null && user.Password == LoginCredential.GetHashedPassword(login.Password, user.Salt))
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
        public IActionResult RestoreLogin(LoginCredential login)
        {
            IActionResult response = Unauthorized();
            bool isTokenValid = ValidateCurrentToken(login.Token);
            User user = _userRepository.getUserById(login.UserId);
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
    }
}
