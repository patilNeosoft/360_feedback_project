using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Configurations
{
    internal class AnnouncementsConfiguration:IEntityTypeConfiguration<Announcements>
    {
        public void Configure(EntityTypeBuilder<Announcements> builder)
        {
            builder
                 .HasKey(b => b.AnnouncementId);
        }

    }
}
