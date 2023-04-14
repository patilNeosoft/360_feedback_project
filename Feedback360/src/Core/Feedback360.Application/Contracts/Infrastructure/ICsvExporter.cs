using Feedback360.Application.Features.Events.Queries.GetEventsExport;
using System.Collections.Generic;

namespace Feedback360.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
