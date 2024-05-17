using Kitchen_Appliances_Backend.Data;
using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.Interfaces;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dataContext;

        public EmployeeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<int> CreateEmployee(CreateEmployeeRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Employee> ListEmployee()
        {
            return _dataContext.Employees.ToList();
        }

        public Task<int> UpdateEmployee(UpdateEmployeeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
