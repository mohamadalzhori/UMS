using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Classes;

namespace UMS.Persistence.Config
{
    internal class ClassEnrollmentConfig : IEntityTypeConfiguration<ClassEnrollment>
    {
        public void Configure(EntityTypeBuilder<ClassEnrollment> builder)
        {
            builder.HasKey(x => new { x.ClassId, x.StudentId });
        }
    }
}
