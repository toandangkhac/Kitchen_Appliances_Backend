﻿using System;
using System.Collections.Generic;

namespace Kitchen_Appliances_Backend.Models;

public partial class CartDetail
{
    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
