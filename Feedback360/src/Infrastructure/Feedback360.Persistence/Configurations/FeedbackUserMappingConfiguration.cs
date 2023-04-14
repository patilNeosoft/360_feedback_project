using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Configurations
{
    public class FeedbackUserMappingConfiguration : IEntityTypeConfiguration<FeedbackUserMapping>
    {
        [ExcludeFromCodeCoverage]
        public void Configure(EntityTypeBuilder<FeedbackUserMapping> builder)
        {
            builder
                .HasKey(b => b.UserFeedbackId);
        }
    }
}
