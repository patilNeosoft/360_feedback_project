using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Configurations
{
    [ExcludeFromCodeCoverage]
    public class RoleConfiguration: IEntityTypeConfiguration<UserRole>
    {
       
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                 .HasKey(b => b.RoleId);
        }
    }
}
