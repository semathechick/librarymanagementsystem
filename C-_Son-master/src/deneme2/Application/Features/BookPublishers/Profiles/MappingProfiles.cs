using Application.Features.BookPublishers.Commands.Create;
using Application.Features.BookPublishers.Commands.Delete;
using Application.Features.BookPublishers.Commands.Update;
using Application.Features.BookPublishers.Queries.GetById;
using Application.Features.BookPublishers.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BookPublishers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BookPublisher, CreateBookPublisherCommand>().ReverseMap();
        CreateMap<BookPublisher, CreatedBookPublisherResponse>().ReverseMap();
        CreateMap<BookPublisher, UpdateBookPublisherCommand>().ReverseMap();
        CreateMap<BookPublisher, UpdatedBookPublisherResponse>().ReverseMap();
        CreateMap<BookPublisher, DeleteBookPublisherCommand>().ReverseMap();
        CreateMap<BookPublisher, DeletedBookPublisherResponse>().ReverseMap();
        CreateMap<BookPublisher, GetByIdBookPublisherResponse>().ReverseMap();
        CreateMap<BookPublisher, GetListBookPublisherListItemDto>().ReverseMap();
        CreateMap<IPaginate<BookPublisher>, GetListResponse<GetListBookPublisherListItemDto>>().ReverseMap();
    }
}