using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Application.Features.UserRoles;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class AnnouncementsRepository : BaseRepository<Announcements>, IAnnouncementsRepository
    {
        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        public AnnouncementsRepository(IMapper mapper, 
            ApplicationDbContext dbContext, ILogger<Announcements> logger) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        
        public async Task DeleteAnnouncementAsync(Announcements announcements)
        {
            _dbContext.Remove(announcements);
            await _dbContext.SaveChangesAsync();
                
        }

        public async Task<UpdateAnnouncementDto> UpdateAnnouncement(long AnnouncementId, UpdateAnnouncementCommand request)
        {
            if (request.IsActive)
            {
                var activeAnnouncement = _dbContext.Announcements.Where(x => x.BankId == request.BankId && x.IsActive == true).FirstOrDefault();

                if (activeAnnouncement != null)
                {

                    var announcementToUpdate = await _dbContext.Announcements.Where(x => x.AnnouncementId == AnnouncementId).FirstOrDefaultAsync();

                    UpdateAnnouncementDto updateAnnouncementDto = new UpdateAnnouncementDto();

                    if (announcementToUpdate != null)
                    {
                        activeAnnouncement.IsActive = false;
                        announcementToUpdate.Message = request.Message;
                        announcementToUpdate.IsActive = request.IsActive;
                        announcementToUpdate.BankId = request.BankId;
                        announcementToUpdate.LastModifiedDate = DateTime.Now;
                        await _dbContext.SaveChangesAsync();

                        updateAnnouncementDto.Succeeded = true;
                        updateAnnouncementDto.SuccessMessage = "Announcement Updated Successfully..";
                        updateAnnouncementDto.AnnouncementId = announcementToUpdate.AnnouncementId;
                        return updateAnnouncementDto;
                    }
                    else
                    {
                        updateAnnouncementDto.Succeeded = false;
                        updateAnnouncementDto.SuccessMessage = "Sorry! Announcement Not Updated..";
                        updateAnnouncementDto.AnnouncementId = AnnouncementId;
                        return updateAnnouncementDto;
                    }
                }
                else
                {
                    var announcementToUpdate = await _dbContext.Announcements.Where(x => x.AnnouncementId == AnnouncementId).FirstOrDefaultAsync();

                    UpdateAnnouncementDto updateAnnouncementDto = new UpdateAnnouncementDto();

                    if (announcementToUpdate != null)
                    {
                        announcementToUpdate.Message = request.Message;
                        announcementToUpdate.IsActive = request.IsActive;
                        announcementToUpdate.BankId = request.BankId;
                        announcementToUpdate.LastModifiedDate = DateTime.Now;
                        await _dbContext.SaveChangesAsync();

                        updateAnnouncementDto.Succeeded = true;
                        updateAnnouncementDto.SuccessMessage = "Announcement Updated Successfully..";
                        updateAnnouncementDto.AnnouncementId = announcementToUpdate.AnnouncementId;
                        return updateAnnouncementDto;
                    }
                    else
                    {
                        updateAnnouncementDto.Succeeded = false;
                        updateAnnouncementDto.SuccessMessage = "Sorry! Announcement Not Updated..";
                        updateAnnouncementDto.AnnouncementId = AnnouncementId;
                        return updateAnnouncementDto;
                    }
                }
            }
            else {
                var announcementToUpdate = await _dbContext.Announcements.Where(x => x.AnnouncementId == AnnouncementId).FirstOrDefaultAsync();

                UpdateAnnouncementDto updateAnnouncementDto = new UpdateAnnouncementDto();

                if (announcementToUpdate != null)
                {
                    announcementToUpdate.Message = request.Message;
                    announcementToUpdate.IsActive = request.IsActive;
                    announcementToUpdate.BankId = request.BankId;
                    announcementToUpdate.LastModifiedDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();

                    updateAnnouncementDto.Succeeded = true;
                    updateAnnouncementDto.SuccessMessage = "Announcement Updated Successfully..";
                    updateAnnouncementDto.AnnouncementId = announcementToUpdate.AnnouncementId;
                    return updateAnnouncementDto;
                }
                else
                {
                    updateAnnouncementDto.Succeeded = false;
                    updateAnnouncementDto.SuccessMessage = "Sorry! Announcement Not Updated..";
                    updateAnnouncementDto.AnnouncementId = AnnouncementId;
                    return updateAnnouncementDto;
                }
            }
            
        }

        public async Task<Announcements> CreateAnnouncement(Announcements request)
        {
            if (request.IsActive)
            {
                var announcementToCreate = _dbContext.Announcements.Where(x => x.BankId == request.BankId && x.IsActive == true).FirstOrDefault();

                if (announcementToCreate != null)
                {
                    request.CreatedDate = DateTime.Now;
                    request.LastModifiedDate = DateTime.Now;
                    announcementToCreate.IsActive = false;
                    await _dbContext.Announcements.AddAsync(request);
                    await _dbContext.SaveChangesAsync();
                    return request;
                }
                else
                {
                    request.CreatedDate = DateTime.Now;
                    request.LastModifiedDate = DateTime.Now;
                    await _dbContext.Announcements.AddAsync(request);
                    await _dbContext.SaveChangesAsync();
                    return request;
                }
            }
            else {
                request.CreatedDate = DateTime.Now;
                request.LastModifiedDate = DateTime.Now;
                await _dbContext.Announcements.AddAsync(request);
                await _dbContext.SaveChangesAsync();
                return request;
            }          
        }



        public async Task<Announcements> FindAnnouncementById(int announcementId)
        {
            Announcements announcement = await _dbContext.Announcements.Where(x => x.AnnouncementId == announcementId).FirstOrDefaultAsync();
            return announcement;
        }

        public async Task<Announcements> FindDashboardAnnouncementById(int bankId)
        {
            Announcements announcement = await _dbContext.Announcements.Where(x => x.BankId == bankId && x.IsActive == true).FirstOrDefaultAsync();
            return announcement;
        }

        public async Task<IReadOnlyList<Announcements>> ListAllAnnouncements()
        {
            return await _dbContext.Announcements.Include(x => x.Bank).ToListAsync();
        }
    }
}
