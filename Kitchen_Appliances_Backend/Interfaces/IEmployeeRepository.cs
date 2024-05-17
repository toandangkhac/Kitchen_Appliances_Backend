using Kitchen_Appliances_Backend.DTO.Employee;
using Kitchen_Appliances_Backend.Models;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> ListEmployee();

        Task<int> CreateEmployee(CreateEmployeeRequest request);

        Task<int> UpdateEmployee(UpdateEmployeeRequest request);

        Task<int> DeleteEmployee(int id);
    }
}
