using BlazorProject.Server.Helpers;
using BlazorProject.Server.Models;
using BlazorProject.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services
{

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id=1,FirstName="Erick",LastName="Kurniawan",
                Username="erick",Password="rahasia" }
        };
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest model)
        {
            var user = _users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user == null) return null;
            var token = generateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}
