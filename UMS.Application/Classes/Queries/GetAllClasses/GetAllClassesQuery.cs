using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Classes.Queries.GetAllClasses
{
    public record GetAllClassesQuery : IRequest<List<ClassDto>>;

}
