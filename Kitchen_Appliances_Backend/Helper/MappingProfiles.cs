using AutoMapper;
using Kitchen_Appliances_Backend.DTO.Account;
using Kitchen_Appliances_Backend.DTO.Bill;
using Kitchen_Appliances_Backend.DTO.CartItem;
using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.DTO.Customer;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.DTO.Image;
using Kitchen_Appliances_Backend.DTO.Order;
using Kitchen_Appliances_Backend.DTO.OrderDetail;
using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.DTO.ProductPrice;
using Kitchen_Appliances_Backend.DTO.Role;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>();

            //Employee
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<UpdateEmployeeRequest, Employee>();

            //Category
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();

            //Account
            CreateMap<Account, AccountDTO>();

            //Product
            CreateMap<Product, ProductDTO>();
            CreateMap<UpdateProductRequest, Product>();
            CreateMap<CreateProductRequest, Product>();

            //Image
            CreateMap<Image, ImageDTO>();
            CreateMap<CreateImageRequest, Image>();
            CreateMap<UpdateImageRequest, Image>();


            //Customer
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<UpdateCustomerRequest, Customer>();

            //CartDetail
            CreateMap<CartDetail, CartDetailDTO>();
            CreateMap<CreateCartDetailRequest, CartDetail>();

            //ProductPrice
            CreateMap<ProductPrice, ProductPriceDTO>();
            CreateMap<UpdateProductPriceRequest, ProductPrice>();

            //Order
            CreateMap<Order, OrderDTO>();

            //Bill
            CreateMap<Bill, ListBillDto>();
            CreateMap<CreateBillRequest, Bill>();

            //Order Detail
            CreateMap<OrderDetail, OrderDetailDTO>();

        }
    }
}
