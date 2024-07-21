using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Users;
using UMS.Persistence;

namespace UMS.Application.Students.GetAllStudents
{
    internal class GetAllStudentsQueryHandler(AppDbContext _constext) : IRequestHandler<GetAllStudentsQuery, List<Student>>
    {

        async Task<List<Student>> IRequestHandler<GetAllStudentsQuery, List<Student>>.Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _constext.Students.ToListAsync(cancellationToken);
        }
    }
}
