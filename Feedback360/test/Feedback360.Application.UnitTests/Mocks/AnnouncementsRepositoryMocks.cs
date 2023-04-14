using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Feedback360.Persistence.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class AnnouncementsRepositoryMocks
    {
        public static Mock<IAnnouncementsRepository> AnnouncementsRepository()
        {
            var mockAnnouncementsRepository = new Mock<IAnnouncementsRepository>();

            var announcementsList = new List<Domain.Entities.Announcements>
            {
                new Domain.Entities.Announcements
                {
                  AnnouncementId =1,
                  Message = "Welcome",
                  IsActive = true,
                  BankId = 1
                },
                new Domain.Entities.Announcements
                {
                  AnnouncementId = 2,
                  Message = "GoodBye",
                  IsActive = false,
                  BankId = 2
                }
            };

            //announcements list
            mockAnnouncementsRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(announcementsList);

            //announcement by Announcement By Id
            mockAnnouncementsRepository.Setup(repo => repo.FindAnnouncementById(It.IsAny<int>())).ReturnsAsync(
                (int Announcement) =>
                {
                    return announcementsList.SingleOrDefault(x => x.AnnouncementId == 1);
                });

            //announcement by Announcement By Bank Id for Dashboard
            mockAnnouncementsRepository.Setup(repo => repo.FindDashboardAnnouncementById(It.IsAny<int>())).ReturnsAsync(
                (int Announcement) =>
                {
                    return announcementsList.SingleOrDefault(x => x.BankId == 1);
                });

            //add new announcement
            mockAnnouncementsRepository.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Announcements>())).ReturnsAsync(

                (Domain.Entities.Announcements announcement) =>
                {
                    Domain.Entities.Announcements newAnnouncement = new Domain.Entities.Announcements();
                    newAnnouncement.Message = "Test1";
                    newAnnouncement.IsActive = false;
                    newAnnouncement.BankId = 1;
                    announcementsList.Add(newAnnouncement);
                    return newAnnouncement;
                });

            //update announcement
            mockAnnouncementsRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Announcements>())).Callback(
                (Domain.Entities.Announcements announcement) =>
                {
                    var oldAnnouncement = announcementsList.First(x => x.AnnouncementId == announcement.AnnouncementId);
                    var index = announcementsList.IndexOf(oldAnnouncement);
                    if (index != -1)
                        announcementsList[index] = oldAnnouncement;
                });

            //delete announcement
            mockAnnouncementsRepository.Setup(repo => repo.DeleteAnnouncementAsync(It.IsAny<Domain.Entities.Announcements>())).Callback(
                (Domain.Entities.Announcements announcements) =>
                {
                    announcementsList.Remove(announcements);
                });

            return mockAnnouncementsRepository;
        }
    }
}
