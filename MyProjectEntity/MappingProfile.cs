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


            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();

            CreateMap<Institute, InstituteViewModel>();
            CreateMap<InstituteViewModel, Institute>();


            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>();

            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();

            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, Department>();


            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();

            CreateMap<Lead, LeadViewModel>();
            CreateMap<LeadViewModel, Lead>();

            CreateMap<Contact, ContactViewModel>();
            CreateMap<ContactViewModel, Contact>();

            CreateMap<UserDomainModel, UserViewModel>();
            CreateMap<UserViewModel, UserDomainModel>();

            CreateMap<PostDomainModel, PostViewModel>();
            CreateMap<PostViewModel, PostDomainModel>();

            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();

            CreateMap<Passport, PassportViewModel>();
            CreateMap<PassportViewModel, Passport>();


            CreateMap<Client, ClientViewModel>();
            CreateMap<ClientViewModel, Client>();

            CreateMap<Lawyer, LawyerViewModel>();
            CreateMap<LawyerViewModel, Lawyer>();
        }
    }
}
