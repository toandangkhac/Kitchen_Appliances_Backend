using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeClient _client;
        public EmployeeController(IEmployeeClient client) 
        {
            _client = client;
        }

        public IActionResult Index()
        {
            List<EmployeeDTO> employees = _client.GetListAll().Result; 
            foreach (EmployeeDTO employee in employees)
            {
                Console.WriteLine(employee.Email);
            }
            return View();
        }

        //ContentResult , ViewResult, JsonResult
        public IActionResult Detail(int id)
        {
            EmployeeDTO employee = _client.GetEmployeeById(id).Result;
            Console.WriteLine(employee.Email);
            return View();
        }

    }
}
