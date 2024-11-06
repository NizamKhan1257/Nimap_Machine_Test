using ShopDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopRepositories.IRepository
{
    public interface IProduct
    {
        public List<ProductDTO> GetProduct();
        public void AddProduct(ProductDTO productDTO);
        public bool UpdateProduct(ProductDTO productDTO);
        public void DeleteProduct(int id);
        public List<CategoryDTO> GetCategories();

        
    }
}
