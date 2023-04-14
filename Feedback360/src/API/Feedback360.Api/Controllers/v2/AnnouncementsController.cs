using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAnnouncementsRepository _announcementsRepository;

        private readonly ILogger _logger;


        public AnnouncementsController(IMediator mediator, ILogger<AnnouncementsController> logger, IAnnouncementsRepository announcementsRepository)
        {

            _announcementsRepository = announcementsRepository;
            _mediator = mediator;
            _logger = logger;

        }

        [HttpGet("AllAnnouncements", Name = "AllAnnouncements")]
        public async Task<ActionResult> GetAllAnnouncements()
        {
            var response = await _mediator.Send(new GetAllAnnouncementsQuery());
            return Ok(response);
        }

        [HttpPost("CreateAnnouncement" , Name = "CreateAnnouncement")]
        public async Task<ActionResult> CreateAnnouncement([FromBody]CreateAnnouncementCommand createAnnouncementCommand) {
            var response = await _mediator.Send(createAnnouncementCommand);
            return Ok(response);
        }

        [HttpPut("updateAnnouncement" , Name = "UpdateAnnouncement")]
        public async Task<ActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementCommand announceCommand)
        {
            var response = await _mediator.Send(announceCommand);
            return Ok(response);
        }

        [HttpDelete("DeleteAnnouncement", Name = "DeleteAnnouncement")]
        public async Task<ActionResult> DeleteAnnouncement(int announcementId)
        {
            var deleteAnnouncementCommand = new DeleteAnnouncementCommand() { AnnouncementId = announcementId};
            var response = await _mediator.Send(deleteAnnouncementCommand);
            return Ok(response);
        }

        [HttpGet("GetAnnouncementById", Name = "GetAnnouncementById")]
        public async Task<ActionResult> GetAnnouncementById(int announcementId)
        {
            var getannouncemntByIdQuery = new GetAnnouncementByIdQuery() { AnnouncementId = announcementId };
            var response = await _mediator.Send(getannouncemntByIdQuery);
            return Ok(response);

        }

        [HttpGet("GetDashboardAnnouncementById", Name = "GetDashboardAnnouncementById")]
        public async Task<ActionResult> GetDashboardAnnouncementById(int bankId)
        {
            var getDashboardAnnouncemntByIdQuery = new GetDashboardAnnouncementByIdQuery() { BankId = bankId };
            var response = await _mediator.Send(getDashboardAnnouncemntByIdQuery);
            return Ok(response);

        }
    }
}
