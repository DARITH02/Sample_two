using Sample_two.Models;
using System.Data.SqlClient;

namespace Sample_two.Data
{
    public class ProductsService
    {
        private readonly string connectionString;

        public ProductsService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Items> getAllProducts()
        {
            List<Items> products = new List<Items>();
            using (SqlConnection conn=new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT P.pro_name,C.cate_name,P.pro_id,P.status,P.codition,P.price,P.img FROM products P LEFT JOIN category C ON C.cate_name=P.cate_name", conn);
                
                 conn.Open();
               SqlDataReader rdr= cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Items items = new Items
                    {
                        Id = Convert.ToInt32(rdr["pro_id"]),
                        Cate_name = rdr["cate_name"].ToString(),
                        Pro_name = rdr["pro_name"].ToString() ,
                        Price = Convert.ToDecimal(rdr["price"]),
                        Status = Convert.ToBoolean(rdr["status"]),
                        Condition = rdr["codition"].ToString()    ,
                        Img_name = rdr["img"].ToString()    ,

                    };
                     products.Add(items);
                } 
            }
            return products;
        }

//select item from category
        public List<Category> GetCategory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                List<Category> categories = new List<Category>();

                SqlCommand cmd = new SqlCommand("SELECT id,cate_name FROM category",conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
 
                while (rdr.Read())
                {
                    Category category = new Category()
                    {
                        Id = Convert.ToInt32(rdr["id"]),
                        Cate_name = rdr["cate_name"].ToString()
                    };
                    categories.Add(category);
                }
                return categories;

            }
        }




        public void Products_Insert(Items items)
        {
            string query = "INSERT INTO products VALUES(@Pro_name,@Price,@Cate_name,@Status,@Condition,@Img)";

            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Pro_name", items.Pro_name);
                cmd.Parameters.AddWithValue("@Price", items.Price);
                cmd.Parameters.AddWithValue("@Cate_name", items.Cate_name);
                cmd.Parameters.AddWithValue("@Status", items.Status);
                cmd.Parameters.AddWithValue("@Condition", items.Condition);
              //cmd.Parameters.AddWithValue("@Img", items.Img);


              /*  byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    items.Img.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();              
                }
*/
                cmd.Parameters.AddWithValue("@Img", items.Img_name);


                conn.Open();
                cmd.ExecuteNonQuery();

            };

        }



        public Items FindId(int id)
        {
            Items item = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE pro_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item = new Items
                    {
                        Id = Convert.ToInt32(reader["pro_id"]),
                        Pro_name = reader["pro_name"].ToString(),
                        Cate_name = reader["cate_name"].ToString(),
                        Price = Convert.ToDecimal(reader["price"]),
                        Status = Convert.ToBoolean(reader["status"]),
                        Condition = reader["codition"].ToString(),
                        Img_name = reader["img"].ToString(),
                    
                    };
                }
            }
            return item;
        }


        //update products
        public void Update(Items items)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE products SET pro_name=@Pro_name,cate_name=@Cate_name,price=@Price,Status=@Status,codition=@Condition,img=@Img WHERE pro_id=@Id", conn);

                cmd.Parameters.AddWithValue("@Id", items.Id);
                cmd.Parameters.AddWithValue("@Pro_name", items.Pro_name);
                cmd.Parameters.AddWithValue("@Cate_name", items.Cate_name);
                cmd.Parameters.AddWithValue("@Price", items.Price);
                cmd.Parameters.AddWithValue("@Status", items.Status);
                cmd.Parameters.AddWithValue("@Condition", items.Condition);
                cmd.Parameters.AddWithValue("@Img", items.Img_name);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //delete data 

        public void Delete(int id)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE pro_id=@Id", conn);

                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();

            }


        }


    }
}
