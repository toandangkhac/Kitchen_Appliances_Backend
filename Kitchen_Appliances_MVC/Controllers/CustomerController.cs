using Kitchen_Appliances_MVC.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_Appliances_MVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerServiceClient _customerClient;

        public CustomerController(ICustomerServiceClient customerClient)
        {
            _customerClient = customerClient;
        }
        public IActionResult Index()
        { 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var res = await _customerClient.GetCustomerById(id);

            return View();
        }
    }
}
