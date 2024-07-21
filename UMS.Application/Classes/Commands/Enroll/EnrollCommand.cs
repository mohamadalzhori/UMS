using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Classes.Commands.Enroll
{
    public record EnrollCommand(
        long StudentId,
        long ClassId) : IRequest;
}
