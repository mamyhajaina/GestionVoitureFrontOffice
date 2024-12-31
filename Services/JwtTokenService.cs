using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace GestionVoitureFrontOffice.Services
{
    public class JwtTokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenService(IConfiguration configuration)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }

        public string GenerateToken(int idUser, string email, string role)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_key);

                Console.WriteLine("---GenerateToken role: " + role);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email), // Ajout de l'email
                    new Claim(ClaimTypes.Role, role),             // Ajout du rôle
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("idUser", idUser.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _issuer,
                    Audience = _audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                //// Sérialisation de l'exception pour le débogage
                //var serializedException = SerializeException(ex);
                //Console.WriteLine("Serialized Exception: " + serializedException);

                //// Retourner un message d'erreur sérialisé
                //return serializedException;
                throw new ArgumentException("Sérialisation de l'exception pour le débogage");
            }
        }

        public string SerializeException(Exception ex)
        {
            var exceptionData = new
            {
                ex.Message,
                ex.StackTrace,
                // Vous pouvez ajouter d'autres propriétés utiles, mais évitez d'exposer des informations sensibles
            };

            // Sérialiser l'exception sans certaines propriétés problématiques
            return JsonSerializer.Serialize(exceptionData);
        }
    }
}
