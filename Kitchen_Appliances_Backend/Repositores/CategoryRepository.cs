using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.Interfaces;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


    }
}
