using Application.Features.Rezervations.Commands.Create;
using Application.Features.Rezervations.Commands.Delete;
using Application.Features.Rezervations.Commands.Update;
using Application.Features.Rezervations.Queries.GetById;
using Application.Features.Rezervations.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.LoanTransactions.Queries.GetList;
using Application.Features.Rezervations.Queries.GetListReservationByMemberId;

namespace Application.Features.Rezervations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rezervation, CreateRezervationCommand>().ReverseMap();
        CreateMap<Rezervation, CreatedRezervationResponse>().ReverseMap();
        CreateMap<Rezervation, UpdateRezervationCommand>().ReverseMap();
        CreateMap<Rezervation, UpdatedRezervationResponse>().ReverseMap();
        CreateMap<Rezervation, DeleteRezervationCommand>().ReverseMap();
        CreateMap<Rezervation, DeletedRezervationResponse>().ReverseMap();
        CreateMap<Rezervation, GetByIdRezervationResponse>().ReverseMap();
        CreateMap<Rezervation, GetListRezervationListItemDto>().ReverseMap();
        CreateMap<GetListRezervationListItemDto, Rezervation>().ReverseMap().ForMember(i => i.BookName, opt => opt.MapFrom(j => j.Book.Name));
        CreateMap<IPaginate<Rezervation>, GetListResponse<GetListRezervationListItemDto>>().ReverseMap();
    }
}