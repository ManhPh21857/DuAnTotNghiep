using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication
{
    public class TokenController : HumanResourcesController
    {
        public TokenController(ISender mediator) : base(mediator)
        {
        }

        //[HttpGet("expire")]
        //public bool CheckTokenExpire()
        //{
        //    return true;
        //}

        [AllowAnonymous]
        [HttpGet("expire/{token}")]
        public bool CheckTokenExpire(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }
    
            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
        //public bool CheckTokenIsValid(string token)
        //{
        //    var tokenTicks = this.GetTokenExpirationTime(token);
        //    var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

        //    var now = DateTime.Now.ToUniversalTime();

        //    var valid = tokenDate >= now;

        //    return valid;
        //}
        //public long GetTokenExpirationTime(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtSecurityToken = handler.ReadJwtToken(token);
        //    var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
        //    var ticks= long.Parse(tokenExp);
        //    return ticks;
        //}
    }
}
