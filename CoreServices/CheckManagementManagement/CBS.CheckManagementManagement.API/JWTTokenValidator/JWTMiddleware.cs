using CBS.CheckManagementManagement.Dto;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CBS.APICaller.Helper
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && !IsTokenExpired(token))
            {
                AttachAccountToContext(context, token);
            }

            await _next(context);
        }

        public static bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jsonToken == null)
                    return true;

                return jsonToken.ValidTo < DateTime.UtcNow;
            }
            catch (Exception)
            {
                return true;
            }
        }
        private void AttachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var jwtConfig = _configuration.GetSection("JwtSettings");
                string key = jwtConfig["key"];
                string issuer = jwtConfig["issuer"];
                string audience = jwtConfig["audience"];
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                var handler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var principal = handler.ValidateToken(token, validationParameters, out validatedToken);
                context.User = principal;

                var userInfoToken = context.RequestServices.GetRequiredService<UserInfoToken>();

                userInfoToken.Id = principal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                userInfoToken.Email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                userInfoToken.BankID = principal.Claims.FirstOrDefault(c => c.Type == "BankID")?.Value;
                userInfoToken.BranchID = principal.Claims.FirstOrDefault(c => c.Type == "BranchID")?.Value;
                userInfoToken.FullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
                userInfoToken.BranchCode = principal.Claims.FirstOrDefault(c => c.Type == "BranchCode")?.Value;
                userInfoToken.BranchName = principal.Claims.FirstOrDefault(c => c.Type == "BranchName")?.Value;
                userInfoToken.BankCode = principal.Claims.FirstOrDefault(c => c.Type == "BankCode")?.Value;

                var claimValue = principal.Claims.FirstOrDefault(c => c.Type == "IsHeadOffice")?.Value;
                if (!string.IsNullOrEmpty(claimValue) && bool.TryParse(claimValue, out var isHeadOffice))
                {
                    userInfoToken.IsHeadOffice = isHeadOffice;
                }

                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    userInfoToken.Token = authHeader.Substring("Bearer ".Length);
                }

                context.Items["Id"] = userInfoToken.Id;
                context.Items["Email"] = userInfoToken.Email;

                context.Session.SetString("FullName", userInfoToken.FullName);
                context.Session.SetString("Token", userInfoToken.Token);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Token validation failed: {ex.Message}");
            }
        }
    }
}
