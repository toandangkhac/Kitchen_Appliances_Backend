﻿using Kitchen_Appliances_MVC.ViewModels.CartDetail;
using Kitchen_Appliances_MVC.ViewModels.Category;
using Kitchen_Appliances_MVC.ViewModels.Customer;
using Kitchen_Appliances_MVC.ViewModels.Product;

namespace Kitchen_Appliances_MVC.ViewModelData.CartDetail
{
	public class CartDetailViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public CustomerDTO Customer { get; set; }
		public List<CartDetailDTO> CartDetails { get; set; }// PId, Quantity
		public List<ProductDTO> Products { get; set; }
		public Dictionary<int, string> Images { get; set; } // idProduct, ImageURL
	}
}
