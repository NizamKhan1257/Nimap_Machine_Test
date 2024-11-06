using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopDTOs;
using ShopRepositories.IRepository;
using PagedList.Core;
using PagedList.Core.Mvc;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        #region ASSIGN FIELD
        private readonly IProduct _product;

        public ShopController(IProduct product)
        {
            _product = product;
        }
        #endregion

        #region PRODUCT GET CONTROLLER
      

        [HttpGet]
        public IActionResult ProductIndex(int? pageNumber)
        {
            var result1 = _product.GetProduct(); 
            int pageSize = 7; 

            
            var pagedList = result1.AsQueryable().ToPagedList(pageNumber ?? 1, pageSize);

            return View(pagedList); 
        }





        #endregion

        #region PRODUCT ADD CONTROLLER
        [HttpGet]
        public IActionResult ProductAdd() 
        {
            var result3 = _product.GetCategories();
            ViewBag.Data = new SelectList(result3, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult ProductAdd(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                
                var result3 = _product.GetCategories();
                ViewBag.Data = new SelectList(result3, "CategoryId", "CategoryName");
                return View(productDTO);
            }

            _product.AddProduct(productDTO);
            return RedirectToAction("ProductIndex", "Shop");
        }
        #endregion

        #region PRODUCT UPDATE CONTROLLER
        [HttpGet]
        public IActionResult ProductUpdate(int id)
        {
            var result2 = _product.GetProduct().FirstOrDefault(model=> model.ProductId == id);
            var result3 = _product.GetCategories();
            ViewBag.Data = new SelectList(result3, "CategoryId", "CategoryName");
            return View(result2);
        }

       
        [HttpPost]
        public IActionResult ProductUpdate(ProductDTO productDTO)
        { 
            _product.UpdateProduct(productDTO);
            return RedirectToAction("ProductIndex", "Shop"); 
        }
       #endregion

        #region PRODUCT DELETE CONTROLLER
        [HttpGet]
        public IActionResult ProductDelete(int id)
        {
            _product.DeleteProduct(id);
            return RedirectToAction("ProductIndex", "Shop");
        }
        #endregion


    }
}
