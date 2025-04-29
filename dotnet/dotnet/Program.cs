
using System.Data.SqlClient;
using System.Security;
using dotnet.Models;    
namespace dotnet;


 public static class Connect
    {
        public static string ConnectionString { get;} = "Data Source=HP\\SQLEXPRESS;Initial Catalog=appdb;Integrated Security=True";
    }

class Program
{
    public static SqlConnection OpenConnection()
    {
        string connectionString = Connect.ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        try
        {
            conn.Open();
            Console.WriteLine("Connection opened successfully.");
            return conn;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening connection: {ex.Message}");
            return null;
        }

    }

    public static void AddCrop(Crop crop)
    {
        try
        {
            if (crop.Price > 0 && crop.Stock > 0)
            {
                string query = "Insert into Crop (CropName,Season,Stock,Price) values (@CropName,@Season,@Stock,@Price)";
                using (SqlCommand cmd = new SqlCommand(query, OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@CropName", crop.CropName);
                    cmd.Parameters.AddWithValue("@Season", crop.Season);
                    cmd.Parameters.AddWithValue("@Stock", crop.Stock);
                    cmd.Parameters.AddWithValue("@Price", crop.Price);

                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Crop has been added!");
                    }
                    else
                    {
                        Console.WriteLine("Crop has not been added!");
                    }

                }
            }
            else
            {
                throw new Exception("Price or Stock must be greater than zero.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Crop not added: {ex.Message}");
        }
    }


    public static void DisplayCrop()
    {
        try
        {
            string query = "SELECT * FROM Crop";
            using (SqlCommand cmd = new SqlCommand(query, OpenConnection()))
            {

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Crop crop = new Crop
                        {
                            CropID = reader.GetInt32(0),
                            CropName = reader.GetString(1),
                            Season = reader.GetString(2),
                            Stock = reader.GetInt32(3),
                            Price = reader.GetDecimal(4),
                            RevenueEstimate = reader.GetDecimal(5)
                        };
                        Console.WriteLine($"CropID: {crop.CropID}, CropName: {crop.CropName}, Season: {crop.Season}, Stock: {crop.Stock}, Price: {crop.Price}, RevenueEstimate: {crop.RevenueEstimate}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error While Displaying: {ex.Message}");
        }
    }

    public static void UpdateCrop(Crop crop)
    {
        try
        {
            if (crop.Price > 0)
            {
                string query = "Update Crop Set CropName=@cropName , Season = @season , Stock=@stock , Price=@price where CropID=@cropID";
                using (SqlCommand cmd = new SqlCommand(query, OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@cropName", crop.CropName);
                    cmd.Parameters.AddWithValue("@season", crop.Season);
                    cmd.Parameters.AddWithValue("@stock", crop.Stock);
                    cmd.Parameters.AddWithValue("@price", crop.Price);
                    cmd.Parameters.AddWithValue("@cropID", crop.CropID);

                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Crop Updated Succesfully!");
                    }
                }
            }
            else
            {
                throw new Exception("Crop Price must be grater than zero!");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error While Update:{ex.Message}");
        }
    }
    public static void Main(string[] args)
    {
        DisplayCrop();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}



    
            
    
