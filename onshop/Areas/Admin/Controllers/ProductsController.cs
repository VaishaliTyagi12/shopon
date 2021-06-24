using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using onshop.Data;
using onshop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace onshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _dbcontext;
        private IHostingEnvironment _he;

        public ProductsController(ApplicationDbContext dbContext, IHostingEnvironment he)
        {
            _dbcontext = dbContext;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_dbcontext.products.Include(a =>a.ProductTypes).ToList());
        }

        //post index method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount,decimal? largeAmount)
        {
            var product = _dbcontext.products.Include(a => a.ProductTypes).Where(a =>a.price>=lowAmount && a.price<=largeAmount).ToList();
            if(lowAmount == null || largeAmount == null)
            {
                product = _dbcontext.products.Include(a => a.ProductTypes).ToList();
            }
            return View(product);
        }

        //get create method
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_dbcontext.productTypes.ToList(), "Pid", "ProductType");
            return View();
        }
        [HttpPost]
        //post create method
        public async Task<IActionResult> Create(Products products,IFormFile image )
        {
            if(ModelState.IsValid)
            {
                var searchproduct = _dbcontext.products.FirstOrDefault(a => a.Name == products.Name);
                if(searchproduct!=null)
                {
                    ViewBag.message = "This product already exists.";
                    ViewData["ProductTypeId"] = new SelectList(_dbcontext.productTypes.ToList(), "Pid", "ProductType");
                    return View(products);
                }

                if(image!=null)
                {
                    //var name = Path.Combine(_he.ContentRootPath+"Images\\",Path.GetFileName(image.FileName));
                    //await image.CopyToAsync(new FileStream(name,FileMode.Create));
                    //products.Image = "Images/" + image.FileName;

                    var uniqueFileName = GetUniqueFileName(image.FileName);
                    var uploads = Path.Combine(_he.WebRootPath, "Images");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if(image==null)
                {
                    //noimage is image name which should be in images folder
                    products.Image = "Images/noimage.png";
                }
                _dbcontext.Add(products);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        //get edit method
        public ActionResult Edit(int? id)
        {
            ViewData["ProductTypeId"] = new SelectList(_dbcontext.productTypes.ToList(), "Pid", "ProductType");
            if(id==null)
            {
                return NotFound();
            }
            var product = _dbcontext.products.Include(a =>a.ProductTypes).FirstOrDefault(a =>a.id==id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //post edit method
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //change webrootpath to contectroothpath
                    //var name = Path.Combine(_he.ContentRootPath + "/Images", Path.GetFileName(image.FileName));
                    //await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    //products.Image = "Images/" + image.FileName;

                    var uniqueFileName = GetUniqueFileName(image.FileName);
                    var uploads = Path.Combine(_he.WebRootPath, "Images");
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (image == null)
                {
                    //noimage is image name which should be in images folder
                    products.Image = "Images/noimage.png";
                }
                _dbcontext.Add(products);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //get details method
        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var product = _dbcontext.products.Include(a => a.ProductTypes).FirstOrDefault(a => a.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //get delete method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbcontext.products.Include(a => a.ProductTypes).Where(a => a.id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View();
        }

        //post delete method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbcontext.products.FirstOrDefault(a => a.id == id);
            if (product == null)
            {
                return NotFound();
            }
            _dbcontext.products.Remove(product);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
