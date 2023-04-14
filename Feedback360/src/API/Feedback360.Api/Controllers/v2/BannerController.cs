    using Feedback360.Application.Features.Banks.Query;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
using Feedback360.Application.Features.Banners.Queries.GetAllBanners;
using Feedback360.Application.Features.Banners.Queries.GetBannerById;
using Feedback360.Application.Features.Banners.Queries.GetBannersByBankId;
using Feedback360.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BannerController : Controller
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public BannerController(IMediator mediator, ILogger<BannerController> logger)
        {

            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetBankList", Name = "GetBankList")]
        public async Task<ActionResult> GetAllBanks()
        {
            var dtos = await _mediator.Send(new GetBankListQuery());
            return Ok(dtos);
        }


        [HttpPost("AddBanner", Name = "AddBanner")]
        public async Task<ActionResult<bool>> AddBanner(CreateBannerCommand createBannerCommand)
        {

            bool IsBannerAdded = false;

            var response = await _mediator.Send(createBannerCommand);
            if ((response.Data.BannerId != null) && (response.Data.BankId != null))
            {
                IsBannerAdded = true;

            }

            return Ok(IsBannerAdded);
        }


        [HttpGet("GetAllBanners", Name = "GetAllBanners")]
        public async Task<ActionResult> GetAllBanners()
        {
            
            var allBanners = await _mediator.Send(new GetAllBannersQuery());
            return Ok(allBanners);
        }

        [HttpGet("GetBannerById", Name = "GetBannerById")]
        public async Task<ActionResult> GetBannerById(int bannerId)
        {
            var getBannerByIdQuery = new GetBannerByIdQuery() { BannerId = bannerId };
            var response = await _mediator.Send(getBannerByIdQuery);
            return Ok(response);

        }

        [HttpDelete("DeleteBanner", Name = "DeleteBanner")]
        public async Task<ActionResult> DeleteBanner(int bannerId)
        {
            var deleteBannerCommand = new DeleteBannerCommand() { BannerId = bannerId };
            var response = await _mediator.Send(deleteBannerCommand);
            return Ok(response);

        }


        [HttpPut("UpdateBanner", Name = "UpdateBanner")]

        public async Task<ActionResult> UpdateBanner(UpdateBannerCommand updateBannerCommand)
        {
            var updatedBanner = await _mediator.Send(updateBannerCommand);
            return Ok(updatedBanner);
        }

        [HttpGet("GetBannersByBankId", Name = "GetBannersByBankId")]
        public async Task<ActionResult> GetBannersByBankId(int bankId)
        {
            var getBannerByBankIdQuery = new GetBannersByBankIdQuery() { BankId = bankId };
            var response = await _mediator.Send(getBannerByBankIdQuery);
            return Ok(response);

        }

    }

}
