using Microsoft.Extensions.Configuration;
using ShopDTOs;
using ShopRepositories.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepositories.Repository
{
    public class ProductRepo : IProduct
    {
        #region CONFIGURATION OR ASSIGN FIELD
        private readonly IConfiguration _config;

        public ProductRepo(IConfiguration configuration)
        {
            _config = configuration;
        }

        private string SqlConnection()
        {
            return _config.GetConnectionString("ShopCS").ToString();
        }
        #endregion

        #region FETCH PRODUCT
        public List<ProductDTO> GetProduct()
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("[dbo].[spGetProduct]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);

            List<ProductDTO> list = new List<ProductDTO>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ProductDTO
                {
                    ProductId = Convert.ToInt32(dr["ProductId"]),
                    ProductName = dr["ProductName"].ToString(),
                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
                    CategoryName = dr["CategoryName"].ToString()
                });
            }
            con.Close();
            return list;
        }
        #endregion

        #region CREATE PRODUCT
        public void AddProduct(ProductDTO productDTO)
        {
            
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("[dbo].[spAddProduct]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductName", productDTO.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", productDTO.CategoryId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        #region UPDATE PRODUCT
        public bool UpdateProduct(ProductDTO productDTO)
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("[dbo].[spUpdateProduct]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", productDTO.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", productDTO.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", productDTO.CategoryId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region DELETE PRODUCT
        public void DeleteProduct(int id)
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("[dbo].[spDeleteProduct]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        #endregion

        #region FETCH CATEGORIES
        public List<CategoryDTO> GetCategories()
        {
            SqlConnection con = new SqlConnection(this.SqlConnection());
            SqlCommand cmd = new SqlCommand("spGetCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);

            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new CategoryDTO
                {
                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
                    CategoryName = dr["CategoryName"].ToString()
                });
            }
            con.Close();
            return list;

        }

        #endregion


    }
}
