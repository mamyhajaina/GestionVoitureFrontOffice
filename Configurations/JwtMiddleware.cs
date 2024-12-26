using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestionVoitureFrontOffice.Configurations
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_key);

                    var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = _issuer,
                        ValidAudience = _audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    }, out SecurityToken validatedToken);

                    var emailClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ??
                             claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

                    context.Items["UserEmail"] = emailClaim;
                    context.Items["UserRole"] = roleClaim;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la validation du token : {ex.Message}");
                }
            }

            await _next(context);
        }
    }

}
