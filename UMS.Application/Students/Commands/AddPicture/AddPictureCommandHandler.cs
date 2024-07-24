using MediatR;
using UMS.Domain.Exceptions.Students;
using UMS.Persistence;

namespace UMS.Application.Students.Commands.AddPicture;

public class AddPictureCommandHandler(AppDbContext _context) : IRequestHandler<AddPictureCommand>
{
    public async Task Handle(AddPictureCommand request, CancellationToken cancellationToken)
    {
        var student = _context.Students.Find(request.StudentId);

        if (student == null)
            throw new StudentNotFound(request.StudentId);

        student.PicturePath = request.Path;

        await _context.SaveChangesAsync();
    }
}