using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Classes.Commands.RegisterClass
{
    public record RegisterClassCommand(
        long TeacherId,
        long CourseId) : IRequest<long>;
}
