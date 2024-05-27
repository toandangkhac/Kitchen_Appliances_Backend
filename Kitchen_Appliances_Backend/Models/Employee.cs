using System;
using System.Collections.Generic;

namespace Kitchen_Appliances_Backend.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Image { get; set; }

    public string Email { get; set; } = null!;

    public virtual Account EmailNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    //public virtual ICollection<Productprice> Productprices { get; set; } = new List<Productprice>();
}
