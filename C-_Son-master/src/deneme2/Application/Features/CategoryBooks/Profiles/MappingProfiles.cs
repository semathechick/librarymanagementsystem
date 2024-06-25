using Application.Features.CategoryBooks.Commands.Create;
using Application.Features.CategoryBooks.Commands.Delete;
using Application.Features.CategoryBooks.Commands.Update;
using Application.Features.CategoryBooks.Queries.GetById;
using Application.Features.CategoryBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.CategoryBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CategoryBook, CreateCategoryBookCommand>().ReverseMap();
        CreateMap<CategoryBook, CreatedCategoryBookResponse>().ReverseMap();
        CreateMap<CategoryBook, UpdateCategoryBookCommand>().ReverseMap();
        CreateMap<CategoryBook, UpdatedCategoryBookResponse>().ReverseMap();
        CreateMap<CategoryBook, DeleteCategoryBookCommand>().ReverseMap();
        CreateMap<CategoryBook, DeletedCategoryBookResponse>().ReverseMap();
        CreateMap<CategoryBook, GetByIdCategoryBookResponse>().ReverseMap();
        CreateMap<CategoryBook, GetListCategoryBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<CategoryBook>, GetListResponse<GetListCategoryBookListItemDto>>().ReverseMap();
    }
}