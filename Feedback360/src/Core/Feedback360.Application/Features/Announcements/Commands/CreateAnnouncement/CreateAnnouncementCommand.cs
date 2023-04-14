using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementCommand : IRequest<Response<CreateAnnouncementDto>>
    {
        public int AnnouncementId { get; set; }
        [Required(ErrorMessage = "Please Enter the Announcement")]
        public string Message { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please Select Bank Name")]
        public int BankId { get; set; }
    }
}
