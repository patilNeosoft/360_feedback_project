﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard
{
    public class GetDashboardAnnouncementByIdVm
    {
        public int AnnouncementId { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        
    }
}
