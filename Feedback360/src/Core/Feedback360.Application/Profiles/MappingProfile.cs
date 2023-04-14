using AutoMapper;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetCommentsById;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetQueryById;
using Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.Banks.Query;
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
using Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard;
using Feedback360.Application.Features.Events.Commands.CreateEvent;
using Feedback360.Application.Features.Events.Commands.Transaction;
using Feedback360.Application.Features.Events.Commands.UpdateEvent;
using Feedback360.Application.Features.Events.Queries.GetEventDetail;
using Feedback360.Application.Features.Events.Queries.GetEventsExport;
using Feedback360.Application.Features.Events.Queries.GetEventsList;
using Feedback360.Application.Features.FeedbackAdmin.Commands.RegisterAdmin;
using Feedback360.Application.Features.FeedbackForm.Commands.AddFeedback;
using Feedback360.Application.Features.FeedbackForm.Commands.EditRepaFeedback;
using Feedback360.Application.Features.FeedbackForm.Commands.EditRevaFeedback;
using Feedback360.Application.Features.FeedbackForm.Commands.GetFeedbackId;
//using Feedback360.Application.Features.FeedbackForm.Queries.GetFeedbacksofRepaAndReva;
using Feedback360.Application.Features.FeedbackForm.Queries.GetQuestionsByBankId;
using Feedback360.Application.Features.Orders.GetOrdersForMonth;
using Feedback360.Application.Features.RolePermission.Command.AddPermission;
using Feedback360.Application.Features.RolePermission.Command.UpdatePermission;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole;
using Feedback360.Application.Features.UserAuthority.Commands.AddAuthority;
using Feedback360.Application.Features.UserAuthority.Queries.GetReporteeByBankId;
using Feedback360.Application.Features.UserAuthority.Queries.GetReviewingAuthorityByBankId;
using Feedback360.Application.Features.UserAuthority.Queries.GetUsersByBankId;
using Feedback360.Application.Features.SelfFeedBack.Query.GetFeedBackByUserId;
using Feedback360.Application.Features.SelfFeedBack.Query.GetSelfFeedbackSummary;
using Feedback360.Application.Features.SelfFeedBack.Query.GetUserAuthorityDataByUserId;
using Feedback360.Application.Features.SelfFeedBack.Query.GetUserFeedbackDataByFinancialYear;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Features.UserQueries.Command.CreateUserQuery;
using Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueries;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueryDataByUserId;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using Feedback360.Application.Features.DepartmentTeams.Command.addEmployeeToGroup;
using Feedback360.Application.Features.DepartmentTeams.Query.GetEmployeeListByDepId;
using Feedback360.Application.Features.DepartmentTeams.Query.GetGroupMembersList;
using Feedback360.Application.Features.DepartmentTeams.Query.GetSecondaryRoleList;
using Feedback360.Application.Features.DepartmentTeams.Query.GetTeamLeader;
using Feedback360.Application.Features.DepartmentTeams.Query.GetActiveFinancialYear;

namespace Feedback360.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {          
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, TransactionCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, StoredProcedureCommand>();
            CreateMap<Category, StoredProcedureDto>();

            CreateMap<Order, OrdersForMonthDto>();
            CreateMap<Bank, GetBankListVM>();
            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();
            
            //mapping for create user role.
            CreateMap<UserRole, CreateUserRoleDto>().ReverseMap();
            CreateMap<UserRole, CreateUserRoleCommand>().ReverseMap();

            //mapping for get all user Roles
            CreateMap<UserRole, GetAllUserRolesQuery>().ReverseMap();
            CreateMap<UserRole, GetAllUserRolesVm>().ReverseMap();

            //mapping for delete user role
            CreateMap<UserRole, DeleteUserRoleCommand>().ReverseMap();
            CreateMap<UserRole, DeleteUserRoleDto>().ReverseMap();

            //mapping for update user role
            CreateMap<UserRole,UpdateUserRoleCommand>().ReverseMap();
            CreateMap<UserRole, UpdateUserRoleDto>().ReverseMap();

            //mapping profile for get user role by Id
            CreateMap<UserRole, GetUserRoleByIdVm>().ReverseMap();
            CreateMap<Event, EventListVm>().ConvertUsing<EventVmCustomMapper>();

            CreateMap<User, RegisterAdminDto>().ReverseMap();
            CreateMap<User, RegisterAdminCommand>().ReverseMap();


            //mapping for create banner
            CreateMap<Banner, CreateBannerCommand>().ReverseMap();
            CreateMap<Banner,CreateBannerDto>().ReverseMap();

            //mapping for get all banners
            CreateMap<Banner, GetAllBannersQuery>().ReverseMap();
            CreateMap<Banner, GetAllBannersVm>().ReverseMap();

            //mapping for delete banner
            CreateMap<Banner, DeleteBannerDto>().ReverseMap();

            //mapping for get banner by Id
            CreateMap<Banner, GetBannerByIdVm>().ReverseMap();

            //mapping for update banner
            CreateMap<Banner, UpdateBannerCommand>().ReverseMap();
            CreateMap<Banner, UpdateBannerDto>().ReverseMap();

            //mapping frofiles for get all banners by bank id
            CreateMap<Banner, GetBannersByBankIdVm>().ReverseMap();


            //mapping for add role permission
            CreateMap<RolePermissionMapping, AddRolePermissionDto>().ReverseMap();
            CreateMap<RolePermission, AddRolePermissionCommand>().ReverseMap();

            //mapping getallpermission
            CreateMap<RolePermission, GetAllPermissionsVM>();

            //mapping get permission by roleid

            CreateMap<GetPermissionByRoleDto, GetPermissionByRoleQueryVM>().ReverseMap();
            //mapping update permissions
            CreateMap<GetPermissionByRoleDto, UpdatePermissionCommand>().ReverseMap();


            //mapping for create new announcement
            CreateMap<Announcements, CreateAnnouncementDto>().ReverseMap();
            CreateMap<Announcements, CreateAnnouncementCommand>().ReverseMap();

            //mapping for get all announcements
            CreateMap<Announcements, GetAllAnnouncementsQuery>().ReverseMap();
            CreateMap<Announcements, GetAllAnnouncementsVm>().ReverseMap();

            //mapping for delete announcement
            CreateMap<Announcements, DeleteAnnouncementCommand>().ReverseMap();
            CreateMap<Announcements, DeleteAnnouncementDto>().ReverseMap();

            //mapping for update announcement
            CreateMap<Announcements, UpdateAnnouncementCommand>().ReverseMap();
            CreateMap<Announcements, UpdateAnnouncementDto>().ReverseMap();
            //mapping for Get Announcement by Id
            CreateMap<Announcements, GetAnnouncementByIdVm>().ReverseMap();

            //mapping for Get Dashboard Announcement by Id
            CreateMap<Announcements, GetDashboardAnnouncementByIdVm>().ReverseMap();
            CreateMap<Announcements, GetAnnouncementByIdVm>().ReverseMap();

            //mapping for create user query
            CreateMap<Query, CreateUserQueryCommand>().ReverseMap();
            CreateMap<Query, CreateUserQueryDto>().ReverseMap();

            //mapping for get userqueries by userId
            CreateMap<Query, GetUserQueryByUserIdVm>().ReverseMap();
            CreateMap<Query, GetUserQueriesQueryVm>().ReverseMap();
            CreateMap<Comment, CreateUserCommentCommand>().ReverseMap();
            CreateMap<Comment, GetAllCommentsVm>().ReverseMap();


            //mapping for adminquery getall
            CreateMap<Query, GetAllAdminQueryVM>().ReverseMap();

            //mapping of adminquery getbyid
            CreateMap<Query, GetQueryByIdVM>().ReverseMap();

            //mapping of getallcommentsbyid
            CreateMap<Comment, GetCommentsByIdQueryDto>().ReverseMap();

            //mapping of UserAuthority for getUsersByBankId
            CreateMap<User, GetUsersByBankIdVM>().ReverseMap();

            //mapping of UserAuthority Query
            CreateMap<User, GetUsersByBankIdQuery>().ReverseMap();

            //mapping of UserAuthorityCommand to add authority for User
            CreateMap<UserAuthorityMapping, AddAuthorityCommand>().ReverseMap();

            //mapping of UserAuthorityDto to add authorityfor User
            CreateMap<UserAuthorityMapping, AddAuthorityDto>().ReverseMap();

            //Mapping for UserAuth Reportee VM
            CreateMap<User, GetReporteeByBankIdVM>().ReverseMap();

            //Mapping for UserAuthQuery for Reportee
            CreateMap<User, GetReporteeByBankIdQuery>().ReverseMap();

            //Mapping for UserAuth Reportee VM
            CreateMap<User, GetReviewingAuthorityByBankIdVM>().ReverseMap();

            //Mapping for UserAuthQuery for Reviewing Authority
            CreateMap<User, GetUserQueriesQuery>().ReverseMap();

            //mapping of getallquestionby bankid
            CreateMap<Questionnaire, GetQuestionsByBankIdQueryVM>().ReverseMap();
            //add feedback
            CreateMap<FeedbackAnswerForm, AddFeedbackCommand>().ReverseMap();
            //getfeedbacks of repa and reva
            // CreateMap<FeedbackForm, GetFeedbacksOfRepaAndRevaDto>().ReverseMap();

            //getfeedbackid from feedbackusermapping after insert
            CreateMap<FeedbackUserMapping, GetFeedbackIdCommand>().ReverseMap();
            //repafeedbacklist
           CreateMap<FeedbackAnswerForm, EditRepaFeedbackCommand>().ReverseMap();
            //revafeedbacklist
            CreateMap<FeedbackAnswerForm, EditRevaFeedbackCommand>().ReverseMap();
            //CreateMap<List<FeedbackAnswerFormVM>, List<FeedbackAnswerForm>>().ReverseMap();
            //mapping for getting authority information by user id
            CreateMap<UserAuthorityMappingVm, GetUserAuthorityDataByUserIdQueryVM>().ReverseMap();

            //mapping for getting feedback data
            CreateMap<GetFeedBackByUserIdQueryVm, UserFeedbackDetailsVm>().ReverseMap();

            CreateMap<GetUserFeedbackDataByFinancialYearVm, UserFeedbackDetailsVm>().ReverseMap();
            CreateMap<GetSelfFeedbackSummaryVm, UserFeedbackSummaryVM>().ReverseMap();
            CreateMap<addEmployeeToGroupCommand, DepartmentTeam>().ReverseMap();
            CreateMap<GetEmployeeListByDepIdVm, EmployeeListInDepVm>().ReverseMap();
            CreateMap<GetGroupMemberListVm, GroupMemberListVm>().ReverseMap();
            
            CreateMap<GetSecondaryRoleListVm, SecondaryRole>().ReverseMap();
            CreateMap<GetTeamLeaderQueryVm, TLDetailsVm>().ReverseMap();
            CreateMap<GetActiveFinancialYearVm, FinancialYear>().ReverseMap();


        }
    }
}
