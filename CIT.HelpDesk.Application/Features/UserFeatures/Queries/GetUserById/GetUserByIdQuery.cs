using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable
namespace CIT.HelpDesk.Application.Features.UserFeatures.Queries.GetUserById
{
    public record GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetUserByIdQueryHAndler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IGenericrepository<User> _userRepository;
        public GetUserByIdQueryHAndler(IGenericrepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           return _userRepository.GetById(request.Id);
        }
    }
}
