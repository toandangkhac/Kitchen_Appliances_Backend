using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Bill;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IBillRepository
    {
        //Tạo bill hóa đơn cho đơn hàng
        Task<ApiResponse<bool>> savePaymentInfor(int orderId);

        //In ra thông tin hóa đơn
        Task<ApiResponse<BillDto>> GetBillInformation(int billId);

        //In danh sách hóa đơn
        Task<ApiResponse<List<ListBillDto>>> GetAllBill();
    }
}
