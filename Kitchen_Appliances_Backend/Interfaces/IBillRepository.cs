using Kitchen_Appliances_Backend.Commons.Responses;
using Kitchen_Appliances_Backend.DTO.Bill;

namespace Kitchen_Appliances_Backend.Interfaces
{
    public interface IBillRepository
    {
        Task<ApiResponse<bool>> savePaymentInfor(CreateBillRequest billRequest);
    }
}
