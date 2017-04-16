using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

using blogAPI.Models;
using blogAPI.Options;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api")]
    public class AuthController : BaseController
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly CredentialsRepository _credentialsRepository;

        public AuthController(IOptions<JwtIssuerOptions> jwtOptions,
                                ILogger<AuthController> logger,
                                CredentialsRepository credentialsRepository)
        {
            _jwtOptions = jwtOptions.Value;

            _logger = logger;

            _credentialsRepository = credentialsRepository;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm]UserCredentials credentials)
        {
            try
            {
                if (await _credentialsRepository.AddUserCredentialsAsync(credentials))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while registrating user{e.Message}");
            }

            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromForm]UserCredentials credentials)
        {
            try
            {
                if (credentials.UserName.Equals(string.Empty))
                    return BadRequest();
            
                var identity = await _credentialsRepository.GetUserCredentialsAsync(credentials.UserName);
                
                if (identity == null || !credentials.Password.Equals(identity.Password))
                {
                    _logger.LogDebug($"Invalid username {credentials.UserName} or password {credentials.Password}");
                    
                    return BadRequest("Invalid credentials");
                }

                var response = new 
                {
                    access = GetAccessToken(credentials.UserName),
                    refresh = GetRefreshToken(credentials.UserName)
                };
                
                return Ok(JsonToString(response));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while authorization\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpGet("refresh")]
        [Authorize(Policy = "Refresh")]
        public IActionResult Refresh()
        {
            try
            {
                return Ok(JsonToString(GetAccessToken(GetClaimByName(SUB))));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while refreshing token\n {e.Message}");
            }

            return BadRequest();
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                                                                                                    .TotalSeconds);
        
        /// <summary>
        /// converte object to json string
        /// </summary>
        /// <param name="json">object to convert</param>
        /// <returns>json string</returns>
        private string JsonToString(Object json) => JsonConvert.SerializeObject(json, _serializerSettings);
        /// <summary>
        /// returns access token to the given user by userName
        /// </summary>
        /// <param name="userName">user's userName</param>
        /// <returns>access token with expiration time</returns>
        private Object GetAccessToken(string userName)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                                                                        ClaimValueTypes.Integer64),
                new Claim("AuthorizedUser", "User")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                accessToken = encodedJwt,
                expiresIn = (int)_jwtOptions.ValidFor.TotalSeconds
            };
            
            return response;
        }
        /// <summary>
        /// returns refresh token to the given user by userName
        /// </summary>
        /// <param name="userName">user's userName</param>
        /// <returns>refresh token</returns>
        private string GetRefreshToken(string userName)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                                                                        ClaimValueTypes.Integer64),
                new Claim("TokenType", "Refresh")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.Add(TimeSpan.FromDays(3650)), // 10 years is enough I guess :D
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return encodedJwt;
        }
    }
}