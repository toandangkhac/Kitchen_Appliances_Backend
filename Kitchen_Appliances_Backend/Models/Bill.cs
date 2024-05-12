using System;
using System.Collections.Generic;

namespace Kitchen_Appliances_Backend.Models;

public partial class Bill
{
    public int OrderId { get; set; }

    public DateTime PaymentTime { get; set; }

    public decimal Total { get; set; }

    public virtual Order Order { get; set; } = null!;
}
