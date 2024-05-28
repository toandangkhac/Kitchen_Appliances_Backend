using System;
using System.Collections.Generic;

namespace Kitchen_Appliances_Backend.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreateAt { get; set; }

    public int CustomerId { get; set; }

    public int Status { get; set; }

    public bool PaymentStatus { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
