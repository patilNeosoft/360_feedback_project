using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
        {
            public void Configure(EntityTypeBuilder<Questionnaire> builder)
            {
                builder
                     .HasKey(b => b.QuestionId);
            }
        }
    
}
