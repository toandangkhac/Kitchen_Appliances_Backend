using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_MVC.Abstractions;
using Kitchen_Appliances_MVC.DTO;
using Kitchen_Appliances_MVC.ViewModels.Employee;
using Kitchen_Appliances_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICustomerServiceClient _customerServiceClient;

        public EmployeeController(ICustomerServiceClient customerServiceClient)
        {
            _customerServiceClient = customerServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _customerServiceClient.ListCustomer();
            Console.WriteLine(res.Message);
            return View();
        }

        //ContentResult , ViewResult, JsonResult
        public async Task<IActionResult> Detail(int id)
        {
            var request = new UpdateProductRequest()
            {
                Name = null,
                Description = null,
                Price = null,
                Quantity = 23
            };

            var res = await _customerServiceClient.DeleteCustomer(id);
            Console.WriteLine(res.Message + " Code: " + res.Status);
            return View();
        }

    }
}
