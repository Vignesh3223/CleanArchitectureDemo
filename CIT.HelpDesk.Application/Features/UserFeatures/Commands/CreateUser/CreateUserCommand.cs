using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using CIT.HelpDesk.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CIT.HelpDesk.WebAPI.Exceptions.GlobalException;

#pragma warning disable
namespace CIT.HelpDesk.Application.Features.UserFeatures.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<ActionResult<Response>>
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ActionResult<Response>>
    {
        private readonly IGenericrepository<User> _userRepository;
        public CreateUserCommandHandler(IGenericrepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<Response>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
           
            User userdata = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };
            if (userdata is null)
            {
                throw new BadRequestException("Your request couldn't be handled");
            }
            else
            {
                _userRepository.Add(userdata);
                var response = new Response
                {
                    StatusCode = 200,
                    Message = "User Created Successfully",
                    Data = userdata
                };
                return new OkObjectResult(response);
            }
        }
    }
}
