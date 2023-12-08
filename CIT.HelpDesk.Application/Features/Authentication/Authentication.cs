using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using CIT.HelpDesk.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static CIT.HelpDesk.WebAPI.Exceptions.GlobalException;

#pragma warning disable
namespace CIT.HelpDesk.Application.Features.Authentication
{
    public record LoginModel : IRequest<ActionResult<Response>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class AuthenticationHandler : IRequestHandler<LoginModel, ActionResult<Response>>
    {
        private readonly IGenericrepository<User> _userRepository;
        public AuthenticationHandler(IGenericrepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<Response>> Handle (LoginModel request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetAll();
            var currentuser = user.FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);
            if (currentuser == null)
            {
                throw new NotFoundException("User not found");
            }
            else
            {
                string jwtToken = GetToken(currentuser);
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    var response = new Response
                    {
                        StatusCode = 200,
                        Message = "Logged In Successfully",
                        Token = jwtToken
                    };
                    return new OkObjectResult(response);
                }
                else
                {
                    throw new BadRequestException("Failed to Generate token");
                }
            }
        }
        public string GetToken(User currentuser)
        {
            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs05Txs";
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, currentuser.Username),
                new Claim(ClaimTypes.Email, currentuser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
