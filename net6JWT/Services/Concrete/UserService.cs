using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using net6JWT.DataAccess.EntityframeworkCore.Abstract;
using net6JWT.Dtos;
using net6JWT.Model;
using net6JWT.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace net6JWT.Services.Concrete
{
    public class UserService : IUserService
    {
        private IAuthDal _authDal;
        private IConfiguration _configuration;

        public UserService(IAuthDal authDal, IConfiguration configuration)
        {
            _authDal = authDal;
            _configuration = configuration;
        }

        public string Login(UserDto user)
        {

            User res = _authDal.Get(c => c.Username == user.Username);
            if (res.Username == "undefined" || res.Username == null)
            {
                return "User not registered";
            }
            string token = CreateToken(user);

            return token;
        }

        public bool Register(UserDto request)
        {

            User res = _authDal.Get(c => c.Username == request.Username);
            if (res != null)
            {
                return false;
            }
            User newUser = new User();

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.Username = request.Username;

            _authDal.Add(newUser);
            return true;
        }

        private string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token) as string;

            return jwt;

        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }

    }
}
