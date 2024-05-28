using AutoMapper;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.DTO.Product;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using System.Collections;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class ProductRepository: IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> CreateProduct(CreateProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products.ToList();
            var productDtos = new List<ProductDTO>();
            products.ForEach(x => productDtos.Add(_mapper.Map<ProductDTO>(x)));
            return productDtos;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return _mapper.Map<ProductDTO>(product);
        }

        public Task<bool> UpdateProduct(int id, UpdateProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
