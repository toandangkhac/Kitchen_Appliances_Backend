﻿namespace Kitchen_Appliances_MVC.ViewModels.OrderDetail
{
	public class OrderDetailDTO
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
