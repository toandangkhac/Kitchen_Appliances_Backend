namespace Kitchen_Appliances_MVC.ViewModels.Employee
{
    public class CreateEmployeeRequest
    {
        public string Fullname { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        //Không thể sử dụng IFormFile vì cái này chỉ để test api
        public string Image { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
