using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sample_two.Data;
using Sample_two.Models;

namespace Sample_two.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsService _DBservice;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ProductsService productsService,IWebHostEnvironment webHostEnvironment)
        {
            _DBservice = productsService;
            _webHostEnvironment = webHostEnvironment;
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
         
            /*            List<Category> categories = new List<Category>();
                        categories = _DBservice.GetCategory();
                        ViewBag.Cate = categories;*/
            //string fileName = FileUpload(items);


            /*          string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                      fileName += Path.GetExtension(items.Img.FileName);

                      string imageFullPath = _webHostEnvironment.WebRootPath + "/images/" + fileName;
      */


            // Validate that the file is not null
            if (items.Img == null || items.Img.Length == 0)
            {
                ModelState.AddModelError("Img", "Please upload an image.");
                return View(items); // Return to the view with the validation error
            }

            // Validate the file extension (you can adjust the allowed extensions as needed)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(items.Img.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("Img", "Only image files are allowed (jpg, jpeg, png, gif).");
                return View(items); // Return to the view with the validation error
            }

            // Generate a unique file name based on the current timestamp
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;

            // Define the target file path
            string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory); // Create the directory if it doesn't exist
            }

            string imageFullPath = Path.Combine(imageDirectory, fileName);

            // Save the uploaded file to the server
            using (var stream = new FileStream(imageFullPath, FileMode.Create))
            {
                items.Img.CopyTo(stream);
            }




          /*  using (var stream = System.IO.File.Create(imageFullPath))
                {
                    items.Img.CopyTo(stream);
                }
*/
            var item1 = new Items
            {
                Cate_name = items.Cate_name,
                Price = items.Price,
                Pro_name = items.Pro_name,
                Condition = items.Condition,
                Status = items.Status,
                Img_name = fileName
            };
            _DBservice.Products_Insert(item1);
                return RedirectToAction("Index");

        
            /*  var items1 = new Items
              {
                  Img = fileName
                 ,Pro_name=items.Pro_name,

              };
              _DBservice.Products_Insert(items);
              return RedirectToAction("Index");*/








        }

   /*     public string FileUpload(Items items)
        {

            *//* string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
             fileName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(items.Img);
             string fileParth = Path.Combine(uploadDir, fileName);
             using(var fileStream=System.IO.File.Create(fileParth))
             {
                 items.Img.CopyTo(fileStream);
             }
     *//*
        





            return fileName;
        }*/
        public IActionResult Edit(int id)
        {
            var products = _DBservice.FindId(id);
            return Json(products);
        }

        [HttpPost]
        public IActionResult Edit(Items items)
        {
            string newFileName = items.Img_name;

            if (items.Img != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(items.Img.FileName);
                string imgFullPath = _webHostEnvironment.WebRootPath + "/images/" + newFileName;

            using(var stream = System.IO.File.Create(imgFullPath))
                {
                    items.Img.CopyTo(stream);
                } 
             

                string oldImgPath = _webHostEnvironment.WebRootPath + "/images/" + items.Img_name;
                System.IO.File.Delete(oldImgPath);
            }

            var itemsUp = new Items
            {
                Id=items.Id,
                Pro_name = items.Pro_name,
                Cate_name = items.Cate_name,
                Price = items.Price,
                Condition = items.Condition,
                Status = items.Status,
                Img_name = newFileName,
            };
            _DBservice.Update(itemsUp);



            return Json(items);

      


      
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            var product = _DBservice.FindId(id);

             string fullPathImg=_webHostEnvironment.WebRootPath +"/images/"+product.Img_name;

            System.IO.File.Delete(fullPathImg);
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
