using AutoMapper;
using Entities;
using EntitiesViewModel;
using MyProjectEntity.Entities;

namespace MyProjectEntity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            //.ForMember(dest => dest.FormattedName, opt => opt.MapFrom(src => $"{src.Name} {src.Capital}"));

            CreateMap<Course, CourseViewModel>();
            CreateMap<CourseViewModel, Course>();


            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();


            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();   
        }
    }
}
