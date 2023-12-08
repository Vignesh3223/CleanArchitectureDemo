using CIT.HelpDesk.Application.Interfaces;
using CIT.HelpDesk.Domain.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable

namespace CIT.HelpDesk.Application.Features.UserFeatures.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<IEnumerable<User>>;
   
    public class GetAllUsersQueryHandler:IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IGenericrepository<User> _userRepository;

        public GetAllUsersQueryHandler(IGenericrepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll();
        }
    }
}

