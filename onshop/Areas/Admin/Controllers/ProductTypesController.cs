using Microsoft.AspNetCore.Mvc;
using onshop.Data;
using onshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _dbcontext;
        public ProductTypesController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            var data = _dbcontext.productTypes.ToList();
            return View(data);
        }

        // Create Get Action Method of Product
        public IActionResult Create()
        {
            return View();
        }

        // create Post Action Method to create the product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.productTypes.Add(productTypes);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(productTypes);
        }

            // Edit Get Action Method of Product
            public IActionResult Edit(int? id)
            {
                if(id==null)
            {
                return NotFound();

            }
            var productTypes = _dbcontext.productTypes.Find(id);
            if(productTypes==null)
            {
                return NotFound();
            }
            return View(productTypes);
            }

            // Edit Post Action Method to Update the product
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(ProductTypes productTypes)
            {
                if (ModelState.IsValid)
                {
                    _dbcontext.Update(productTypes);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                return View(productTypes);
            }
        // Details Get Action Method of Product
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var productTypes = _dbcontext.productTypes.Find(id);
            if (productTypes == null)
            {
                return NotFound();
            }
            return View(productTypes);
        }

        // Details Post Action Method to show the details of the product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {

            return RedirectToAction(nameof(Index));
        }

        // Delete Get Action Method of Product
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();

            }
            var productTypes = _dbcontext.productTypes.Find(id);
            if (productTypes == null)
            {
                return NotFound();
            }
            return View(productTypes);
        }

        // Delete Post Action Method to delete the product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if(id==null)
            {
                return NotFound();
            }
            
            if (id!= productTypes.Pid)
            {
                return NotFound();
            }
            var producttype = _dbcontext.productTypes.Find(id);
            if (ModelState.IsValid)
            {
                _dbcontext.Remove(productTypes);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(productTypes);



        }
    }
}
