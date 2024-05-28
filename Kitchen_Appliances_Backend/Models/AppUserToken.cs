using System;
using System.Collections.Generic;

namespace Kitchen_Appliances_Backend.Models;

public partial class AppUserToken
{
    public int? Id { get; set; }

    public string? Token { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public string? Type { get; set; }

    public string? AccountId { get; set; }

    public virtual Account? Account { get; set; }
}
