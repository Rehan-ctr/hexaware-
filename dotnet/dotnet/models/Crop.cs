using System.Diagnostics.Contracts;

namespace dotnet.Models;
	public class Crop
	{
	

		public int CropID { get; set; }
		public string  CropName{ get; set; }
		public string Season{ get; set; }
		public int Stock { get; set; }
		public decimal Price { get; set; }
		public decimal RevenueEstimate { get; set; }
		
		public Crop() { }	
    public	Crop( string cropName,string season,int stock, decimal price ){
			CropName = cropName;
			Season = season;
			Stock = stock;
			Price = price;
		}
		public	Crop( string cropName,string season,int stock, decimal price, int cropID ){
			CropName = cropName;
			Season = season;
			Stock = stock;
			Price = price;
			CropID= cropID;
		}
	}