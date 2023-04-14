﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetCommentsById
{
    public class GetCommentsByIdQueryDto
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public int QueryId { get; set; }
        public string RoleName { get; set; }
    }
}
