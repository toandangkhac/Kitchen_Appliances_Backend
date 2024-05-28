namespace Kitchen_Appliances_MVC.ViewModels.Employee
{
    public class CreateEmployeeRequest
    {
        public string Fullname { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public IFormFile Image { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
