using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.Interfaces;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class OrderdetailRepository : IOrderdetailRepository
    {

        private readonly DataContext _context;

        public OrderdetailRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrderDetails(List<int> cartDetailIds)
        {
            foreach(int i in cartDetailIds)
            {
                var cartDetail = await _context.CartDetails.FindAsync(i);

            }
            throw new NotImplementedException();
        }
    }
}
