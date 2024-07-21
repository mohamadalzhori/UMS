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
    internal class ClassConfig : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");
            builder.HasKey(x => x.Id);

            // teacher classes configured in teacherConfig

            // course classes configured in courseConfig
        
            builder.HasMany(x=> x.ClassEnrollments)
                .WithOne(x=> x.Class)
                .HasForeignKey(x=> x.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x=> x.Sessions)
                .WithOne(x=> x.Class)
                .HasForeignKey(x=> x.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
