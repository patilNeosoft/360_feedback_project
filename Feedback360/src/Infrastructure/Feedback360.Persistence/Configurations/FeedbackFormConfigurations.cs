using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Feedback360.Persistence.Configurations
{
    [ExcludeFromCodeCoverage]
    public class FeedbackFormConfigurations : IEntityTypeConfiguration<FeedbackForm>
    {

        public void Configure(EntityTypeBuilder<FeedbackForm> builder)
        {
            builder
                .HasKey(b => b.FeedbackId);
        }
    }
}

