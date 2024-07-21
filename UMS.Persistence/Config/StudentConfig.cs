using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Users;

namespace UMS.Persistence.Config
{
    internal class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(x => x.Id);

            builder.ComplexProperty(x => x.Name);
            builder.ComplexProperty(x => x.Email);

            builder.HasMany(x=> x.ClassEnrollments)
                .WithOne(x=> x.Student)
                .HasForeignKey(x=> x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
