using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMS.Domain.Courses;
using UMS.Domain.Shared;

namespace UMS.Persistence.Config
{
    internal class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(x => x.Id);

            builder.ComplexProperty(x => x.Name);

            builder.ComplexProperty(x => x.MaxStudentsNumber);

            // no need to configure start and end date, they are configured by convention as optional dateonly

            builder.HasMany(x => x.Classes)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
