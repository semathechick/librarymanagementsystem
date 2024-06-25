using Application.Features.AuthorBooks.Commands.Create;
using Application.Features.AuthorBooks.Commands.Delete;
using Application.Features.AuthorBooks.Commands.Update;
using Application.Features.AuthorBooks.Queries.GetById;
using Application.Features.AuthorBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.AuthorBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuthorBook, CreateAuthorBookCommand>().ReverseMap();
        CreateMap<AuthorBook, CreatedAuthorBookResponse>().ReverseMap();
        CreateMap<AuthorBook, UpdateAuthorBookCommand>().ReverseMap();
        CreateMap<AuthorBook, UpdatedAuthorBookResponse>().ReverseMap();
        CreateMap<AuthorBook, DeleteAuthorBookCommand>().ReverseMap();
        CreateMap<AuthorBook, DeletedAuthorBookResponse>().ReverseMap();
        CreateMap<AuthorBook, GetByIdAuthorBookResponse>().ReverseMap();
        CreateMap<AuthorBook, GetListAuthorBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthorBook>, GetListResponse<GetListAuthorBookListItemDto>>().ReverseMap();
    }
}