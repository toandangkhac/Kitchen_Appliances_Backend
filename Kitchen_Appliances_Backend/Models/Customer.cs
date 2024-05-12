using System;
using System.Collections.Generic;

namespace Kitchen_Appliances_Backend.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Account EmailNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
