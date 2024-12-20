using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Assignment.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }



        #region customer
        public IActionResult CustomerPartial()
        {
            

            return PartialView("_CustomerPartial");
        }

        public IActionResult Customer(int? Id)
        {
            // Initialize the view model
            customerview viewModel = new customerview();

            // Fetch all customers from the database
            viewModel.CustomerList = _db.Customers.ToList();

            if (Id.HasValue)
            {
                // Fetch the customer details for the provided ID
                viewModel.Customer_obj = _db.Customers.FirstOrDefault(c => c.Customer_Id == Id);

                // If no customer with the provided ID is found, return a NotFound result
                if (viewModel.Customer_obj == null)
                {
                    // Return a partial view with a null model or an empty model
                    return PartialView("_CustomerPartial", viewModel); // Pass the entire view model
                }
                else
                {
                    // Return the partial view with the customer data
                    return View("_CustomerPartial", viewModel); // Pass the entire view model, not just Customer_obj
                }
            }

            // Return the full view model to the main view
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Customer(Customer obj)
        {
            if (obj.Name == obj.Phone.ToString())
            {
                ModelState.AddModelError("name", "The phone cannot same with name");
            }
            if (ModelState.IsValid )
            {
                _db.Customers.Add(obj);
                _db.SaveChanges();
            }
          
            return RedirectToAction("Customer");
        }

        [HttpPost]
        public IActionResult Update(Customer obj,int Id )
        {

            obj.Customer_Id = Id;
            if (obj.Name == obj.Phone.ToString())
            {
                ModelState.AddModelError("name", "The phone cannot be the same as the name");
            }

            if (ModelState.IsValid)
            {
                _db.Customers.Update(obj);
                _db.SaveChanges();
            }

            return RedirectToAction("Customer");
        }

        public IActionResult Delete(int? Id)
        {
            Customer? obj = _db.Customers.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Customers.Remove(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Customer");
        }
        #endregion




    }
}
