using Feedback360.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace Feedback360.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<Response<IEnumerable<CategoryListVm>>>
    {
    }
}
