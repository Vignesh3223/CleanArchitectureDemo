using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using CIT.HelpDesk.Shared.Response;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CIT.HelpDesk.WebAPI.Exceptions.GlobalException;

#pragma warning disable
namespace CIT.HelpDesk.Application.Features.UserFeatures.Commands.UpdateUser
{
    public record UpdateUserCommand : IRequest<ActionResult<Response>>
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ActionResult<Response>>
    {
        private readonly IGenericrepository<User> _userRepository;
        public UpdateUserCommandHandler(IGenericrepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ActionResult<Response>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(Convert.ToInt32(request.Id));
            if (user != null)
            {
                user.Username = request.Username;
                user.Email = request.Email;
                user.Password = request.Password;
                _userRepository.Update(user);
                var response = new Response
                {
                    StatusCode = 200,
                    Message = "User Updated Successfully",
                    Data = user
                };
                return new OkObjectResult(response);
            }
            else
            {
                throw new BadRequestException("Your request couldn't be handled");
            }
        }
    }
}
