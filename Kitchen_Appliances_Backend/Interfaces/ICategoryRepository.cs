﻿using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Category;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ApiResponse<object>> GetCategoryById(int id);

        Task<ApiResponse<object>> CreateCategory(CreateCategoryRequest request);

        Task<ApiResponse<object>> UpdateCategory(int id, UpdateCategoryRequest request);

        Task<ApiResponse<object>> DeleteCategory(int id);

        Task<ApiResponse<object>> GetAllCategories();
    }
}
