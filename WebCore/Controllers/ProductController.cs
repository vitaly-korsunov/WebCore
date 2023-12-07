using DalCore;
using DalCore.Repository.IRepsitory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelCore;
using ModelCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Controllers
{
    
        public class ProductController : Controller
        {
            private readonly IProductRepository  _prodRepo;
            public ProductController(IProductRepository prodRepo)
            {
                _prodRepo = prodRepo;
            }
            public async Task<IActionResult> Index()
            {
            IEnumerable<Product> objList = await _prodRepo.GetAll(includeProperties: "Category");
               
                return View(objList);
            }
            public async Task< IActionResult> Upsert(int? id)
            {
            
            ProductVM productVM = new ModelCore.ViewModels.ProductVM()
                {
                    Product = new Product(),
                      CategorySelectList =  _prodRepo.GetAllDropDownList("Category")
                  };
                if (id == null)
                {
                    // this is For create
                    return View(productVM);
                }
                else
                {
                    productVM.Product = await _prodRepo.Find(id.GetValueOrDefault());
                    if (productVM.Product == null)
                    {
                        return NotFound();
                    }
                    return View(productVM);
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Upsert(ProductVM productVM)
            {
                if (ModelState.IsValid)
                {
                    if (productVM.Product.Id == 0)
                    {
                        // create
                     await   _prodRepo.Add(productVM.Product);

                    }
                    else
                    {
                    _prodRepo.Update(productVM.Product);
                    }
                     await _prodRepo.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
                }
            productVM.CategorySelectList = _prodRepo.GetAllDropDownList("Category");
                
                return View(productVM);
            }
        }
   
}
