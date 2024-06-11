using AutoMapper;
using Kitchen_Appliances_Backend.Commons.Exceptions;
using Kitchen_Appliances_Backend.Commons.Responses;
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

        public async Task<ApiResponse<object>> CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);

                _dataContext.Categories.Add(category);
                await _dataContext.SaveChangesAsync();
                
                return new ApiResponse<object>(200, "Tạo Category thành công", true);
            }
            catch (Exception) {
                return new ApiResponse<object>(400, "Create category bị lỗi", false);
            }
        }

        public async Task<ApiResponse<object>> DeleteCategory(int id)
        {
            try
            {
                var category = _dataContext.Categories.Find(id);

                if (category == null)
                {
                    return new ApiResponse<object>(404, "Không tìm thấy category", false);
                }

                var products = _dataContext.Products.Where(x => x.CategoryId == id);
                if (!products.IsNullOrEmpty())
                {
                    return new ApiResponse<object>(400,"No delete category because it exist product", false);
                }
                _dataContext.Categories.Remove(category);
                await _dataContext.SaveChangesAsync();
                return new ApiResponse<object>(200, "Xóa category thành công", true);
            }
            catch (Exception)
            {
                return new ApiResponse<object>(400, "Xóa category bị lỗi", false);
            }
        }

        public async Task<ApiResponse<object>> GetAllCategories()
        {
            var categorieDtos = _mapper.Map<List<CategoryDTO>>(_dataContext.Categories.ToList());

            var res = new ApiResponse<object>(200, "Lấy danh sách category thành công", categorieDtos);
            return res;
        }

        public async Task<ApiResponse<object>> GetCategoryById(int id)
        {
            var category = _dataContext.Categories.Find(id);
            
            if(category == null)
            {
                return new ApiResponse<object>(404, "Không tìm thấy category", _mapper.Map<CategoryDTO>(category));
            }

            return new ApiResponse<object>(200, "Lấy category thành công", _mapper.Map<CategoryDTO>(category));
        }

        public async Task<ApiResponse<object>> UpdateCategory(int id,UpdateCategoryRequest request)
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
                return new ApiResponse<object>(200, "Update category thành công", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>(200, "Update category bị lỗi", false );
            }
        }
    }
}
