using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateCategory(CreateCategoryRequest request)
        {
            bool isSuccess = false;
            try
            {
                var category = _mapper.Map<Category>(request);

                _dataContext.Categories.Add(category);
                await _dataContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception) { 
            }
            
            return isSuccess;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            bool isSuccess = false;
            try
            {
                var category = _dataContext.Categories.Find(id);

                if (category == null)
                {
                    throw new NotFoundException("Category don't exists");
                }

                var products = _dataContext.Products.Where(x => x.CategoryId == id);
                if (!products.IsNullOrEmpty())
                {
                    throw new InvalidRequestException("No delete category because it exist product");
                }
                _dataContext.Categories.Remove(category);
                await _dataContext.SaveChangesAsync();
                isSuccess=true;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public List<Category> GetAllCategories()
        {
            return _dataContext.Categories.ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return _dataContext.Categories.Find(id);
        }

        public async Task<bool> UpdateCategory(int id,UpdateCategoryRequest request)
        {
            bool isSuccess = false;
            try
            {
                var category = _dataContext.Categories.Find(id);

                if (category == null)
                {
                    throw new NotFoundException();
                }
                category.Name = request.Name;
                _dataContext.Categories.Update(category);
                await _dataContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }
    }
}
