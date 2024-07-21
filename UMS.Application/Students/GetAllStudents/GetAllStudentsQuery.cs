using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Users;

namespace UMS.Application.Students.GetAllStudents
{
    public record GetAllStudentsQuery : IRequest<List<Student>>;
}
