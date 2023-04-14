using Feedback360.Application.Contracts.Persistence;
using Feedback360.Infrastructure.EncryptDecrypt;
using Feedback360.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Feedback360.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserPermissionRepository,UserPermissionRepository>();
            services.AddScoped<IAnnouncementsRepository, AnnouncementsRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IQueryRepository, QueryRepository>();    
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IUserAuthorityRepository, UserAuthorityRepository>();
            services.AddScoped<ISelfFeedbackRepository, SelfFeedbackRepository>();
            services.AddScoped<IFeedbackFormRepository, FeedbackFormRepository>();
            services.AddScoped<IDepartmentTeamRepository, DepartmentTeamRepository>();

            return services;
        }
    }
}
