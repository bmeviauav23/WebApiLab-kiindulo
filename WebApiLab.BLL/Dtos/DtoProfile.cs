using AutoMapper;

namespace WebApiLab.Bll.Dtos;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
        CreateMap<Dal.Entities.Product, Dtos.Product>()
            .ReverseMap();
        CreateMap<Dal.Entities.Order, Dtos.Order>()
            .ReverseMap();
        CreateMap<Dal.Entities.Category, Dtos.Category>()
            .ReverseMap();
    }
}
