using Feedback360.Application.Features.Events.Queries.GetEventsExport;
using Feedback360.Infrastructure.FileExport;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Feedback360.Infrastructure.UnitTests.FileExport
{
    public class CsvExporterTests
    {
        [Fact]
        public void ExportEventsToCsv()
        {
            var exporter = new CsvExporter();

            var result = exporter.ExportEventsToCsv(new List<EventExportDto>());

            result.ShouldNotBeNull();
            result.ShouldBeOfType<byte[]>();
        }
    }
}
