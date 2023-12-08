using CIT.HelpDesk.Application.Features.UserFeatures.Commands.CreateUser;
using CIT.HelpDesk.Application.Features.UserFeatures.Commands.DeleteUser;
using CIT.HelpDesk.Application.Features.UserFeatures.Commands.UpdateUser;
using CIT.HelpDesk.Application.Features.UserFeatures.Queries.GetAllUsers;
using CIT.HelpDesk.Application.Features.UserFeatures.Queries.GetUserById;
using CIT.HelpDesk.Domain.Entities;
using CIT.HelpDesk.Shared.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#nullable disable

namespace CIT.HelpDesk.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Register(CreateUserCommand newUser)
        {
            return await _mediator.Send(newUser);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateUser(int? id, UpdateUserCommand updatedUser)
        {
            if (id is not null)
            {
                updatedUser.Id = (int)id;
            }
            return await _mediator.Send(updatedUser);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteUser(int? id)
        {
            if (id is not null)
            {
                return await _mediator.Send(new DeleteUserCommand { Id = (int)id });
            }
            return null;
        }
    }
}
