using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Email).IsRequired(true).HasMaxLength(100);

            builder.ComplexProperty(x => x.Name, p =>
            {
                p.Property(a => a.FirstName).IsRequired(true).HasMaxLength(50);
                p.Property(a => a.LastName).IsRequired(true).HasMaxLength(50);
            });
            
        }
    }
}
