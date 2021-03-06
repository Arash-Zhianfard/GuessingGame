using Abstraction.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Abstraction.Interfaces.Services;

namespace Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerService _playerService;
        private readonly IEncryptService _encryptService;
        private readonly JwtSetting _JwtSetting;
        public AuthService(IPlayerService playerService, IOptions<JwtSetting> jwtSetting, IEncryptService encryptService)
        {
            _playerService = playerService;
            _JwtSetting = jwtSetting.Value;
            _encryptService = encryptService;
        }

        public async Task<JwtInfo?> SignIn(string username, string password)
        {
            var encryptedPass = _encryptService.GenerateMd5String(password);
            var userInfo = await _playerService.GetAsync(username, encryptedPass);

            if (userInfo != null)
            {
                return GenereateToken(new Claim[]
                {
                      new Claim("userid", userInfo.Id.ToString())
                });
            }
            return null;
        }

        public async Task<JwtInfo?> SignUp(SignupRequest signupRequest)
        {
            var encryptedPass = _encryptService.GenerateMd5String(signupRequest.Password);
            const int baseAccountScore = 10000;
            var userInfo = await _playerService.AddAsync(new Player
            {
                UserName = signupRequest.Username,
                Password = encryptedPass,
                Score = baseAccountScore
            });

            if (userInfo != null)
            {
                return GenereateToken(new Claim[]
                {
                      new Claim("userid", userInfo.Id.ToString())
                });
            }

            return null;
        }

        private JwtInfo GenereateToken(Claim[] claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JwtSetting.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokendes = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokendes);
            return new JwtInfo() { Token = token };
        }

        public TokenInfo GetTokenInfo(string authToken)
        {
            if (authToken == null)
            {
                throw new ArgumentNullException();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var userid = tokenHandler.ReadJwtToken(authToken)?.Claims?.FirstOrDefault(x => x.Type == "userid").Value;
            return new TokenInfo() { UserId = int.Parse(userid) };
        }

        public bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSetting.SecretKey))
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }
    }
}
