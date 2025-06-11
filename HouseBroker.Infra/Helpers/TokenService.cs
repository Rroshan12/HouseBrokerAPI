using HouseBroker.Domain.Models;
using HouseBroker.Infra.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HouseBroker.Infra.Helpers
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(string username, string descriptionRole, Guid id, Guid roleid);
        TokenDataDtos GetTokenData(string sessiontokenstring);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;


        //private readonly ISchoolService _schoolService;
        public JwtTokenService(IConfiguration config, DbManagerContext context)
        {
            _config = config;

        }

        public async Task<string> GenerateToken(string username, string descriptionRole,  Guid id, Guid roleid)
        {


            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("UserId", id.ToString()),
                new Claim("RoleId", roleid.ToString()),
                new Claim("RoleDescription", descriptionRole), // Assuming a default role, you can modify this as needed

            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            var tokenOptions = new JwtSecurityToken(
              issuer: _config["JWT:Issuer"],
              audience: null,
              expires: DateTime.Now.AddMinutes(int.Parse(_config["JWT:TokenValidityInMinutes"])),
              claims: claims,
              signingCredentials: creds
          );



            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public TokenDataDtos GetTokenData(string sessiontokenstring)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(sessiontokenstring);

            // Convert the claims to a JSON object (JObject)
            var jsonObject = new JObject();
            foreach (var claim in jwtToken.Claims)
            {
                jsonObject[claim.Type] = claim.Value;
            }
            string jsonString = JsonConvert.SerializeObject(jsonObject);
            TokenDataDtos myJsonObject = JsonConvert.DeserializeObject<TokenDataDtos>(jsonString);

            return myJsonObject;

        }

    }
}
