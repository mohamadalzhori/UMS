using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Shared;

namespace UMS.Application.Students.CreateStudent
{
    public record CreateStudentCommand(Name Name, Email Email) : IRequest<long>;
}
