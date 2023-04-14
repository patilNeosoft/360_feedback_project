using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
using Feedback360.Application.Features.Banners.Queries.GetAllBanners;
using Feedback360.Application.Features.Banners.Queries.GetBannerById;
using Feedback360.Application.Features.Banners.Queries.GetBannersByBankId;
using Feedback360.Application.Features.Categories.Commands.CreateCategory;
using Feedback360.Application.Features.Categories.Commands.StoredProcedure;
using Feedback360.Application.Features.Categories.Queries.GetCategoriesList;
using Feedback360.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Feedback360.Application.Features.Events.Commands.CreateEvent;
using Feedback360.Application.Features.Events.Commands.DeleteEvent;
using Feedback360.Application.Features.Events.Commands.Transaction;
using Feedback360.Application.Features.Events.Commands.UpdateEvent;
using Feedback360.Application.Features.Events.Queries.GetEventDetail;
using Feedback360.Application.Features.Events.Queries.GetEventsExport;
using Feedback360.Application.Features.Events.Queries.GetEventsList;
using Feedback360.Application.Features.Orders.GetOrdersForMonth;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Application.Features.RolePermission.Command.AddPermission;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Feedback360.Application.Features.UserQueries.Command.CreateUserQuery;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueries;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard;

namespace Feedback360.API.UnitTests.Mocks
{
    public class MediatorMocks
    {
        public static Mock<IMediator> GetMediator()
        {
            var mockMediator = new Mock<IMediator>();

            mockMediator.Setup(m => m.Send(It.IsAny<GetCategoriesListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<CategoryListVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetCategoriesListWithEventsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<CategoryEventListVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<CreateCategoryDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<StoredProcedureCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<StoredProcedureDto>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetEventsListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<EventListVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetEventDetailQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<EventDetailVm>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetEventsExportQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new EventExportFileVm() { Data = Encoding.UTF8.GetBytes(new string(' ', 100)), ContentType = "text/csv", EventExportFileName = "Filename"  });
            mockMediator.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<Guid>());
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<Guid>());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Unit());
            mockMediator.Setup(m => m.Send(It.IsAny<TransactionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<Guid>());

            mockMediator.Setup(m => m.Send(It.IsAny<GetOrdersForMonthQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PagedResponse<IEnumerable<OrdersForMonthDto>>(null, 10, 1, 2));
            //mediater for get all user roles
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllUserRolesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<GetAllUserRolesVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetUserRoleByIdQuery>(),It.IsAny<CancellationToken>())).ReturnsAsync(new GetUserRoleByIdVm());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteUserRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<DeleteUserRoleDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateUserRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<UpdateUserRoleDto>());
            mockMediator.Setup(m=>m.Send(It.IsAny<IUserRepository>(), It.IsAny<CancellationToken>())).ReturnsAsync(new AuthResponse());

            //mediator for all announcements operations
            //mockMediator.Setup(m => m.Send(It.IsAny<GetAllAnnouncementsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<GetAllAnnouncementsVm>>());
            mockMediator.Setup(m => m.Send(It.IsAny<CreateAnnouncementCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<CreateAnnouncementDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetAnnouncementByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetAnnouncementByIdVm());
            mockMediator.Setup(m => m.Send(It.IsAny<GetDashboardAnnouncementByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetDashboardAnnouncementByIdVm());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteAnnouncementCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteAnnouncementDto());
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateAnnouncementCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateAnnouncementDto());
            mockMediator.Setup(m => m.Send(It.IsAny<IAnnouncementsRepository>(), It.IsAny<CancellationToken>())).ReturnsAsync(new AuthResponse());

            mockMediator.Setup(m => m.Send(It.IsAny<CreateBannerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<CreateBannerDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteBannerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteBannerDto());
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateBannerCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateBannerDto());
            mockMediator.Setup(m => m.Send(It.IsAny<GetBannerByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetBannerByIdVm());
            mockMediator.Setup(m => m.Send(It.IsAny<GetBannersByBankIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<GetBannersByBankIdVm>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetBannersByBankIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<GetBannersByBankIdVm>());

            mockMediator.Setup(m => m.Send(It.IsAny<AddRolePermissionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllPermissionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<GetAllPermissionsVM>>());
            
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserQueryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<CreateUserQueryDto>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetUserQueriesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<GetUserQueriesQueryVm>());
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommentCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<bool>());
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllCommentsByQueryIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<GetAllCommentsVm>());



            mockMediator.Setup(m => m.Send(It.IsAny<GetAllAdminQueryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Response<IEnumerable<GetAllAdminQueryVM>>());
            return mockMediator;
        }
    }
}
