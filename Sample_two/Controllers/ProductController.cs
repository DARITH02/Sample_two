using Microsoft.AspNetCore.Mvc;
using Sample_two.Data;
using Sample_two.Models;

namespace Sample_two.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsService _DBservice;
        public ProductController(ProductsService productsService)
        {
            _DBservice = productsService;
        }

        public IActionResult Index()
        {
                List<Items> items = _DBservice.getAllProducts();

            List<Category> categories = new List<Category>();
            categories = _DBservice.GetCategory();
            ViewBag.Cate = categories;

            return View(items);
        }

       public IActionResult Create() {

            List<Category> categories = new List<Category>();
            categories = _DBservice.GetCategory();
            ViewBag.Cate = categories;


            return View();
        }

        [HttpPost]
        public IActionResult Create(Items items)
        {

            if (ModelState.IsValid)
            {
                _DBservice.Products_Insert(items);
                return RedirectToAction("Index");
            }
            return View(items);
        }

        public IActionResult Edit(int id)
        {
            var products = _DBservice.FindId(id);
            return Json(products);
        }

        [HttpPost]
        public IActionResult Edit(Items items)
        {
            if (ModelState.IsValid)
            {
                _DBservice.Update(items);
                return Json(items);

            }
            return Json(items);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _DBservice.Delete(id);
            var status = 200;
            return Json(status);

        }
        //Get View Detail

        public IActionResult ViewDetail(int id) {

     var viewDetail=_DBservice.FindId(id);

            return Json(viewDetail);
        
        
        }



    }
}
