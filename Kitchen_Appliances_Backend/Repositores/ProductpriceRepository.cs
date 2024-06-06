using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.ProductPrice;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductPriceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductPriceDTO> ListProductImageByProduct(int productId)
        {
            return _mapper.Map<List<ProductPriceDTO>>(_context.ProductPrices.Where(x => x.ProductId == productId));
        }

        public async Task<int> UpdateProductPrice(UpdateProductPriceRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new NotFoundException("Khong tim thay productId: " + request.ProductId);
            }

            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            var productPrice = new ProductPrice()
            {
                ProductId = request.ProductId,
                AppliedDate = DateTime.Now,
                Employee = employee,
                Price = request.Price,
                EmployeeId = employee.Id
            };

            product.Price = request.Price;
            _context.ProductPrices.Add(productPrice);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return productPrice.Id;
        }
    }
}
