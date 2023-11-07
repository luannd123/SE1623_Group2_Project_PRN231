using AutoMapper;
using DataAccess.DTO.Order;
using DataAccess.DTO.OrderDetail;
using DataAccess.DTO.Product;
using DataAccess.DTO.User;
using DataAccess.Models;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product , ProductDTO>().ReverseMap();
            CreateMap<Product, CreateUpdateProductDTO>().ReverseMap();

            CreateMap<Order , OrderDTO>().ReverseMap();
            CreateMap<Order, CreateUpdateOrderDTO>().ReverseMap();

            CreateMap<User , UserDTO>().ReverseMap();
            CreateMap<User , CreateUpdateUserDTO>().ReverseMap();

            CreateMap<OrderDetail , OrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, CreateUpdateOrderDetailDTO>().ReverseMap();
        }
    }
}
