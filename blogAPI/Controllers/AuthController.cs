using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

using blogAPI.Models;
using blogAPI.Options;
using blogAPI.Data;

namespace WebApiJwtAuthDemo.Controllers
{
    [Route("api")]
    public class AuthController : Controller
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
            ThrowIfInvalidOptions(_jwtOptions);

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
                if (await _credentialsRepository.AddUserCredentials(credentials))
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
            var identity = await GetClaimsIdentity(credentials);
            if (identity == null)
            {
            _logger.LogInformation($"Invalid username ({credentials.UserName}) or password ({credentials.Password})");
            return BadRequest("Invalid credentials");
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, credentials.UserName),
            new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
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
            access_token = encodedJwt,
            expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
            throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
            throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// IMAGINE BIG RED WARNING SIGNS HERE!
        /// You'd want to retrieve claims through your claims provider
        /// in whatever way suits you, the below is purely for demo purposes!
        /// </summary>
        private static Task<ClaimsIdentity> GetClaimsIdentity(UserCredentials user)
        {
            if (user.UserName == "test" &&
                user.Password == "test")
            {
            return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"),
                new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}