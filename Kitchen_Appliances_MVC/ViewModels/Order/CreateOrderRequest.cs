namespace Kitchen_Appliances_MVC.ViewModels.Order
{
    public class CreateOrderRequest
    {
        //Tạo đơn hàng dàng cho người dùng, mã nhân viên chỉ được ghi khi có nhân viên vào xác nhận
        //public int EmployeeId { get; set; }
        public int CustomerId { get; set; }

        public string AddressShipping { get; set; }
    }
}
