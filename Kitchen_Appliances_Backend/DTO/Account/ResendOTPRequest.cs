using Kitchen_Appliances_Backend.Commons.Enums;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kitchen_Appliances_Backend.DTO.Account;

public class ResendOTPRequest
{
    public string Email { get; set; }
    //[JsonIgnore]
    public string Type { get; set; }
}
