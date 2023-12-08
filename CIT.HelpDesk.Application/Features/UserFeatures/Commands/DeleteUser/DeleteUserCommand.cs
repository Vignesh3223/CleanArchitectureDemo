using CIT.HelpDesk.Application.Features.UserFeatures.Commands.UpdateUser;
using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using CIT.HelpDesk.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CIT.HelpDesk.WebAPI.Exceptions.GlobalException;

#pragma warning disable
namespace CIT.HelpDesk.Application.Features.UserFeatures.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest<ActionResult<Response>>
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ActionResult<Response>>
    {
        private readonly IGenericrepository<User> _userRepository;

        public DeleteUserCommandHandler(IGenericrepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<Response>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(Convert.ToInt32(request.Id));
            if (user != null)
            {
                _userRepository.Delete(user);
                var response = new Response
                {
                    StatusCode = 200,
                    Message = "User found and deleted",
                };
                return new OkObjectResult(response);
            }
            else
            {
                throw new NotFoundException("User not found or already deleted");
            }
        }
    }
}
